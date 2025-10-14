using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Lib;
using UnityEngine;

public class LoadSceneAfterInitialize : MonoConstruct
{
    [SerializeField] private SceneField _sceneField;

    private IEnumerator Start()
    {
        var _awaitList = GetComponents<IInitializeCheck>();
        while (_awaitList.Any(i => !((IInitializeCheck)i).IsInitialized))
            yield return null;
        
        context.Resolve<AddressableService>().LoadSceneAsync(_sceneField, true);
    }
}
