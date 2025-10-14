#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public static class ProjectDependencyInstaller
{
    private static readonly List<string> Packages = new()
    {
        // "https://github.com/dbrizov/NaughtyAttributes.git#upm",
        "com.unity.editorcoroutines",
        // "com.tayx.graphy", - найти адреса
        // "sc.utilities.selectionhistory", - найти адреса
        // "https://github.com/GucioDevs/SimpleMinMaxSlider.git#upm",
        "https://github.com/Leopotam/ecslite-di.git",
        "https://github.com/Leopotam/ecslite-unityeditor.git",
        "https://github.com/Hafune/ecslite.git",
        "https://github.com/Hafune/UI-Toolkit-Plus-public-field.git",
        // "com.unity.cinemachine",
        "com.unity.inputsystem"
        // Add other package URLs here
    };

    private static AddRequest _addRequest;
    private static ListRequest _listRequest;
    private static Queue<string> _packagesToInstall;
    private static string _currentPackage;

    [MenuItem("Auto/Project Bootstrap/Run ProjectDependencyInstaller")]
    public static void InstallPackages()
    {
        Debug.Log("ProjectDependencyInstaller Run");
        _listRequest = Client.List(); // Get the list of installed packages
        EditorApplication.update += CheckListRequest;
    }

    private static void CheckListRequest()
    {
        if (!_listRequest.IsCompleted)
            return;

        EditorApplication.update -= CheckListRequest;

        if (_listRequest.Status == StatusCode.Failure)
        {
            Debug.LogError("Failed to list packages: " + _listRequest.Error.message);
            return;
        }

        var installedPackages = _listRequest.Result;
        var existingPackages = new HashSet<string>(installedPackages.SelectMany(p =>
            new[]
            {
                p.name, p.packageId
            }));

        _packagesToInstall = new Queue<string>();

        foreach (var url in Packages)
        {
            if (!existingPackages.Any(i => i.Contains(url)))
            {
                _packagesToInstall.Enqueue(url);
            }
        }

        if (_packagesToInstall.Any())
        {
            InstallNextPackage();
        }
        else
        {
            Debug.Log("All packages are already installed.");
        }
    }

    private static void InstallNextPackage()
    {
        if (_packagesToInstall.Count == 0)
        {
            Debug.Log("All packages have been installed.");
            return;
        }

        _currentPackage = _packagesToInstall.Dequeue();
        _addRequest = Client.Add(_currentPackage);
        EditorApplication.update += CheckAddRequest;
    }

    private static void CheckAddRequest()
    {
        if (!_addRequest.IsCompleted)
            return;

        EditorApplication.update -= CheckAddRequest;

        if (_addRequest.Status == StatusCode.Failure)
        {
            Debug.LogError("Failed to add package: " + _currentPackage + " - " + _addRequest.Error.message);
        }
        else
        {
            Debug.Log("Successfully added package: " + _currentPackage);
        }

        InstallNextPackage();
    }
}
#endif