using System;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class AnimationEventsCopier
    {
        [MenuItem("Auto/Copy Animation Events By Selected (from-to)")]
        private static void CopyAnimationEvents()
        {
            if (Selection.objects.Length < 2)
            {
                Debug.Log("No objects selected.");
                return;
            }
            
            var _fromClip = Selection.objects[0] as AnimationClip;
            var toClip = Selection.objects[1] as AnimationClip;
            
            if (!_fromClip || !toClip)
            {
                Debug.Log("No clips selected.");
                return;
            }
            
            if (toClip == null || _fromClip == null || toClip == _fromClip)
            {
                Debug.LogWarning("ReplaceClip or Clip is null.");
                return;
            }

            float originalLength = _fromClip.length;
            float newLength = toClip.length;

            var originalEvents = _fromClip.events;
            var newEvents = new AnimationEvent[originalEvents.Length];
            
            for (int i = 0; i < originalEvents.Length; i++)
            {
                var originalEvent = originalEvents[i];
                var newEvent = new AnimationEvent
                {
                    functionName = originalEvent.functionName,
                    time = Math.Min(newLength, originalEvent.time / originalLength * newLength),
                    stringParameter = originalEvent.stringParameter,
                    floatParameter = originalEvent.floatParameter,
                    intParameter = originalEvent.intParameter,
                    objectReferenceParameter = originalEvent.objectReferenceParameter,
                    messageOptions = originalEvent.messageOptions
                };
                newEvents[i] = newEvent;
            }

            AnimationUtility.SetAnimationEvents(toClip, newEvents);
            EditorUtility.SetDirty(toClip);
            AssetDatabase.SaveAssets();
            Debug.Log("Clip replaced and events copied with normalized timing successfully.");
        }
    }
}