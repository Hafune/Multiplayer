using UnityEngine;

namespace Core.Lib
{
    public class StringToHash : MonoBehaviour
    {
        [SerializeField] private string _value;

        private void OnValidate() => Debug.Log(_value + ": " + Animator.StringToHash(_value), this);
    }
}