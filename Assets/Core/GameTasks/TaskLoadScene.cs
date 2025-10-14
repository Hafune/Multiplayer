using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Core.Tasks
{
    [Obsolete("Эта задача должна быть последней в цепочке!")]
    public class TaskLoadScene : MonoConstruct, IMyTask
    {
        [SerializeField] private SceneField _scene;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            onComplete?.Invoke();
            context.Resolve<AddressableService>().LoadSceneAsync(_scene);
        }

        [MyButton]
        private void EditorLoadScene() => SceneManager.LoadScene(_scene);
    }
}