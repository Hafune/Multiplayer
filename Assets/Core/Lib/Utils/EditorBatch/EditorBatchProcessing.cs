#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Lib;
using Core.Lib.Utils;
using Lib;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;

public class EditorBatchProcessing : MonoBehaviour
{
    [SerializeField] private Object[] _folders;
    [SerializeField] private GameObject[] _prefabs;
    private int _prefabIndex;
    private int _processorIndex;
    private EditorBatchFilter[] _processors;

    [MyButton]
    private void Run() => EditorApplication.delayCall += OpenPrefab;

    [MyButton]
    private void SetFolderPrefabs()
    {
        _prefabs = Array.Empty<GameObject>();

        foreach (var folder in _folders)
            _prefabs = _prefabs.Concat(GetFolderPrefabs(folder)).ToArray();
    }

    private IEnumerable<GameObject> GetFolderPrefabs(Object folder)
    {
        // Получаем путь к папке в проекте
        var folderPath = AssetDatabase.GetAssetPath(folder);
        if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError("Выбранный объект не является папкой!");
            return Array.Empty<GameObject>();
        }

        // Получаем все файлы в папке с расширением .prefab
        var prefabPaths = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);
        var prefabs = new List<GameObject>();

        foreach (var prefabPath in prefabPaths)
        {
            // Загружаем объект как GameObject
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
                prefabs.Add(prefab);
        }

        return prefabs;
    }

    private void OpenPrefab()
    {
        _prefabIndex = -1;
        NextPrefab();
    }

    private void NextPrefab()
    {
        _prefabIndex++;

        if (_prefabIndex >= _prefabs.Length)
        {
            Debug.Log("Процессоры завершены");
            return;
        }

        ProcessorStart();
    }

    private void ProcessorStart()
    {
        _processorIndex = 0;
        PrefabStageUtility.OpenPrefab(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(_prefabs[_prefabIndex]));
        EditorFunctions.SetAutoSave(false);
        _processors = transform.GetSelfChildrenComponents<EditorBatchFilter>();
        ProcessorNext();
    }

    private void ProcessorNext()
    {
        if (_processorIndex >= _processors.Length)
        {
            Completed();
            return;
        }

        var processor = _processors[_processorIndex++];

        if (!processor.gameObject.activeSelf)
        {
            ProcessorNext();
            return;
        }

        processor.RunProcessing(ProcessorNext, () =>
        {
            StageUtility.GoBackToPreviousStage();
            Debug.Log("Processors break");
        });
    }

    private void Completed()
    {
        EditorFunctions.SetAutoSave(true);
        EditorCoroutineUtility.StartCoroutine(CompletedPrivate(), gameObject);
    }

    private IEnumerator CompletedPrivate()
    {
        yield return null;

        bool hasError = true;
        float wait = 10;

        while (hasError && wait > 0)
        {
            wait -= Time.deltaTime;

            try
            {
                StageUtility.GoBackToPreviousStage();
                hasError = false;
            }
            catch (Exception)
            {
                hasError = true;
            }

            yield return null;
        }

        if (wait <= 0)
            throw new Exception("StageUtility.GoBackToPreviousStage(); ошибка, закончилось время попыток");

        yield return null;

        NextPrefab();
    }
}

#endif