using Lib;
using UnityEngine;

public class LookAtCameraEffect : MonoConstruct
{
    private Transform _cameraTransform;

    private void Awake() => _cameraTransform = context.Resolve<Camera>().transform;

    private void FixedUpdate() => transform.rotation = _cameraTransform.rotation;

    //Метод для аниматора
    private void OnAnimationEnded() => gameObject.SetActive(false);
}