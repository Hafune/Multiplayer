using Core.Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Services
{
    [RequireComponent(typeof(CompositeCollider2D), typeof(Rigidbody2D))]
    public class GeometryPostProcess : MonoBehaviour, IEnvironmentPostProcess
    {
        [SerializeField] private CompositeCollider2D _composite;

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _composite = GetComponent<CompositeCollider2D>();
            _composite.generationType = CompositeCollider2D.GenerationType.Manual;
        }

        private void Awake() => Assert.IsNotNull(_composite);

        public void PostProcess(GameObject root)
        {
            //Костыль - при инстансе префаба с колайдером он не обрабатывается в CompositeCollider2D в этом же кадре
            //Такой Refresh помогает сделать их доступными сразу.
            foreach (var child in GetComponentsInChildren<Collider2D>())
            {
                child.gameObject.SetActive(false);
                child.gameObject.SetActive(true);
            }

            _composite.GenerateGeometry();
        }
    }
}