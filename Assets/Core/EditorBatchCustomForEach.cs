#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EditorBatchCustomForEach : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        Debug.Log(suitable.Length);
        foreach (var i in suitable)
        {
            foreach (Transform child in i)
                child.localPosition = Vector3.zero;

            EditorUtility.SetDirty(i);
            yield return null;
        }

        callback?.Invoke();
    }
}

#endif