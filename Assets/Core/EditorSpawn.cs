using Core.Lib;
using Reflex.Injectors;
using UnityEngine;


public class EditorSpawn : MonoBehaviour
{
    [SerializeField] private Transform _prefab;

#if UNITY_EDITOR
    [MyButton]
    private void Run() => EditorRuntimeContextAccess.Context.Instantiate(_prefab, transform.position, Quaternion.identity);
#endif
}