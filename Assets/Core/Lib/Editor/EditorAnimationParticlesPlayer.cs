using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Lib;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lib
{
    [InitializeOnLoad]
    public class EditorAnimationParticlesPlayer
    {
        private static EditorCoroutine _coroutine;
        private static PrefabStage _prefabStage;
        private static readonly List<GameObject> _spawnedObjects = new();

        static EditorAnimationParticlesPlayer()
        {
            PrefabStage.prefabStageClosing += _ =>
            {
                Clear();
                EditorApplication.delayCall += OpenPrefabStageIfExist;
            };

            PrefabStage.prefabStageOpened += stage =>
            {
                Clear();
                ListenAnimationWindow(stage);
            };

            OpenPrefabStageIfExist();
        }

        private static void Clear()
        {
            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);

            _coroutine = null;

            foreach (var spawnedObject in _spawnedObjects)
                Object.DestroyImmediate(spawnedObject);

            _spawnedObjects.Clear();
            _prefabStage = null;
        }

        private static void OpenPrefabStageIfExist()
        {
            Clear();
            var stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (!stage)
                return;

            ListenAnimationWindow(stage);
        }

        private static void ListenAnimationWindow(PrefabStage prefabStage)
        {
            if (Application.isPlaying)
                return;

            if (_coroutine is not null)
                EditorCoroutineUtility.StopCoroutine(_coroutine);

            _prefabStage = prefabStage;
            _coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(ListenAnimationWindowCoroutine());
        }

        private static IEnumerator ListenAnimationWindowCoroutine()
        {
            AnimationWindow animationWindow = null;

            Reinitialize:

            while (!EditorWindow.HasOpenInstances<AnimationWindow>())
                yield return null;

            animationWindow = animationWindow != null
                ? animationWindow
                : EditorWindow.GetWindow<AnimationWindow>();

            while (!animationWindow.playing && EditorWindow.HasOpenInstances<AnimationWindow>())
                yield return null;

            var clip = animationWindow.animationClip;

            if (!clip)
            {
                yield return null;
                goto Reinitialize;
            }

            var events = clip?.events;

            if (events is null || events.Length == 0)
            {
                yield return null;
                goto Reinitialize;
            }

            var sentEvents = new HashSet<AnimationEvent>();
            float lastTime = 0;

            foreach (var o in _spawnedObjects)
            foreach (var player in o.GetComponentsInChildren<EditorParticlePlayer>())
                player.SetTimeScale(1);

            while (true)
            {
                if (!EditorWindow.HasOpenInstances<AnimationWindow>())
                    goto Reinitialize;

                if (clip != animationWindow.animationClip)
                    goto Reinitialize;

                if (!animationWindow.playing)
                {
                    foreach (var o in _spawnedObjects)
                    foreach (var player in o.GetComponentsInChildren<EditorParticlePlayer>())
                        player.SetTimeScale(0);

                    goto Reinitialize;
                }

                if (lastTime > animationWindow.time)
                {
                    lastTime = 0;
                    sentEvents.Clear();
                }

                float deltaTime = animationWindow.time - lastTime;
                lastTime = animationWindow.time;

                if (_prefabStage)
                    foreach (var animationEvent in events)
                        if (animationEvent.time + deltaTime <= animationWindow.time && sentEvents.Add(animationEvent))
                            SpawnEffect(_prefabStage.prefabContentsRoot, animationEvent, animationWindow);

                yield return null;
            }
        }

        private static void SpawnEffect(GameObject root, AnimationEvent animationEvent, AnimationWindow animationWindow)
        {
            if (!animationEvent.objectReferenceParameter ||
                animationEvent.objectReferenceParameter is not ReferencePath path)
                return;

            var obj = path.Find(root.transform);
            if (obj is null)
                return;

            // Ищем любой компонент SpawnEffect или SpawnEffectOld
            dynamic spawner = obj.GetComponent<SpawnEffect>() ?? (dynamic)obj.GetComponent<SpawnEffectOld>();

            if (spawner == null)
                return;

            // Используем рефлексию для доступа к приватным полям
            var spawnerType = spawner.GetType();
            var prefabField = spawnerType.GetField("_prefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var offsetField = spawnerType.GetField("_globalOffset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var attachEffectField = spawnerType.GetField("_attachEffect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var spawnMethod = spawnerType.GetMethod("EditorPlayParticle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (prefabField == null || offsetField == null || attachEffectField == null || spawnMethod == null)
                return;

            var prefab = (Transform)prefabField.GetValue(spawner);
            var hasParticles = (bool)prefab.GetComponent<ParticleSystem>();

            if (!hasParticles)
                return;

            var attachEffect = attachEffectField!.GetValue(spawner);
            Vector3 offset = (Vector3)offsetField!.GetValue(spawner);
            var instance = (GameObject)spawnMethod!.Invoke(spawner, null);
                
            _spawnedObjects.Add(instance);
            EditorCoroutineUtility
                .StartCoroutineOwnerless(
                    SyncPosition(
                        instance,
                        spawner.transform,
                        spawner.transform.position,
                        attachEffect,
                        offset,
                        animationWindow));
        }

        private static IEnumerator SyncPosition(
            GameObject instance,
            Transform spawner,
            Vector3 startPosition,
            dynamic attachEffect,
            Vector3 startOffset,
            AnimationWindow animationWindow
        )
        {
            var defaultLocalPosition = spawner.transform.localPosition;
            var startLocalPosition = spawner.localPosition;
            var worldSpaceParticles = instance.GetComponentsInChildren<ParticleSystem>()
                .Where(p => p.main.simulationSpace == ParticleSystemSimulationSpace.World).ToArray();

            while (instance &&
                   instance.activeSelf &&
                   animationWindow.rootVisualElement.visible &&
                   animationWindow.previewing)
            {
                var offset = spawner.transform.localPosition - defaultLocalPosition;
                instance.transform.position =
                    attachEffect == Core.Lib.SpawnEffect.AttachEffect.AsChild |
                    attachEffect == Core.Lib.SpawnEffect.AttachEffect.SynchronizePositions
                        ? spawner.position + startOffset
                        : startPosition + offset + startOffset;

                if (startLocalPosition != spawner.localPosition)
                {
                    var delta = spawner.localPosition - startLocalPosition;
                    startLocalPosition = spawner.localPosition;

                    for (int a = 0, iMax = worldSpaceParticles.Length; a < iMax; a++)
                    {
                        var particleSystem = worldSpaceParticles[a];
                        var count = particleSystem.particleCount;
                        var particles = new ParticleSystem.Particle[count];
                        particleSystem.GetParticles(particles);

                        for (int i = 0; i < particles.Length; i++)
                            particles[i].position += delta;

                        particleSystem.SetParticles(particles);
                    }
                }

                yield return null;
            }

            _spawnedObjects.Remove(instance);
            Object.DestroyImmediate(instance);
        }
    }
}