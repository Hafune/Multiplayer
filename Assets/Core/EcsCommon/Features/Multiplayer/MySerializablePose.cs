// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.TransitionLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Gathers the animation details from a character to save
    /// and applies them back after loading.
    /// </summary>
    /// <remarks>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/mixers/serialization">
    /// Animation Serialization</see>
    /// </remarks>
    [Serializable]
    public class MySerializablePose
    {
        /************************************************************************************************************************/

        [SerializeField] private float _RemainingFadeDuration;

        // [SerializeField] private float _SpeedParameter;
        [SerializeField] private List<StateData> _States = new();

        /************************************************************************************************************************/

        [Serializable]
        public struct StateData
        {
            /************************************************************************************************************************/

            /// <summary>The index of the state in the <see cref="TransitionLibrary"/>.</summary>
            /// <remarks>
            /// This is a <c>byte</c> because a library probably won't have more than 256 transitions.
            /// If it does, you would need a <c>ushort</c> instead.
            /// </remarks>
            public byte index;

            /// <summary><see cref="Time"/></summary>
            public float time;

            /// <summary><see cref="AnimancerNode.Weight"/></summary>
            public float weight;

            public float fade;

            /************************************************************************************************************************/
        }

        /************************************************************************************************************************/

        public void GatherFrom(AnimancerComponent animancer) //, StringReference speedParameter)
        {
            _States.Clear();
            _RemainingFadeDuration = 0;

            var activeStates = animancer.Layers[0].ActiveStates;
            for (int i = 0; i < activeStates.Count; i++)
            {
                var state = activeStates[i];

                var clip = state.Clip;

                if (clip)
                {
                    WriteState(animancer, state);
                }
                else
                {
                    foreach (var childState in state)
                    {
                        if (!childState.Clip)
                            continue;

                        WriteState(animancer, childState);
                    }
                }
            }

            float totalWeight = 0f;
            for (int i = _States.Count - 1; i >= 0; i--)
                totalWeight += _States[i].weight;

            for (int i = _States.Count - 1; i >= 0; i--)
            {
                var stateData = _States[i];
                stateData.weight *= 1 / totalWeight;
                _States[i] = stateData;
            }

            // _SpeedParameter = animancer.Parameters.GetFloat(speedParameter);
        }

        private void WriteState(AnimancerComponent animancer, AnimancerState state)
        {
            var raw = -1;

            if (state.Key is null)
            {
                if (state.EffectiveWeight == 0)
                    return;

                for (int j = 0; j < animancer.Graph.Transitions.Count; j++)
                {
                    animancer.Graph.Transitions.TryGetTransition(j, out var transition);
                    if (transition.Transition.Key is ClipTransition asset)
                        if (asset.Clip == state.Clip)
                        {
                            raw = j;
                            break;
                        }
                }

                if (raw == -1)
                    Debug.LogError("State not found");
            }

            var i = _States.Count;
            raw = raw != -1 ? raw : animancer.Graph.Transitions.IndexOf(state.Key);
            var index = (byte)raw;

            _States.Add(new StateData()
            {
                index = index,
                time = state.Time,
                weight = state.EffectiveWeight,
                fade = state.FadeGroup?.RemainingFadeDuration ?? 0f,
            });

            if (state.FadeGroup != null &&
                state.TargetWeight == 1)
            {
                _RemainingFadeDuration = state.FadeGroup.RemainingFadeDuration;

                // If this state is fading in, swap it with the first state
                // so we know which one it is after deserialization.
                if (i > 0)
                    (_States[0], _States[i]) = (_States[i], _States[0]);
            }
        }

        /************************************************************************************************************************/

        public void ApplyTo(AnimancerComponent animancer) //, StringReference speedParameter)
        {
            float weightlessThreshold = AnimancerLayer.WeightlessThreshold;
            try
            {
                AnimancerLayer.WeightlessThreshold = 0;

                AnimancerLayer layer = animancer.Layers[0];
                layer.Stop();
                layer.Weight = 1;
                _RemainingFadeDuration = 0;
                AnimancerState firstState = null;

                for (int i = _States.Count - 1; i >= 0; i--)
                {
                    StateData stateData = _States[i];
                    if (!animancer.Graph.Transitions.TryGetTransition(
                            stateData.index,
                            out TransitionModifierGroup transition))
                    {
                        Debug.LogError(
                            $"Transition Library '{animancer.Transitions}'" +
                            $" doesn't contain transition index {stateData.index}.",
                            animancer);
                        continue;
                    }
                    
                    // Проверяем, проигрывается ли сейчас анимация с таким же клипом
                    float time = stateData.time;
                    float weight = stateData.weight;
                    var activeStates = layer.ActiveStates;
                    for (int j = 0; j < activeStates.Count; j++)
                    {
                        if (weight > 0 && ReferenceEquals(activeStates[j].Clip, transition.Transition.Key) && activeStates[j].IsPlaying)
                        {
                            time = activeStates[j].Time;
                            weight = activeStates[j].Weight;
                            break;
                        }
                    }

                    AnimancerState state = layer.GetOrCreateState(transition.Transition);

                    if (state.Weight != 0)
                        state = layer.GetOrCreateWeightlessState(state);

                    state.IsPlaying = true;
                    state.Time = time;
                    state.SetWeight(weight);

                    if (i != 0)
                        continue;

                    firstState = state;
                    _RemainingFadeDuration = stateData.time;
                }

                if (firstState != null)
                    layer.Play(firstState, Mathf.Max(_RemainingFadeDuration, .1f));
            }
            finally
            {
                AnimancerLayer.WeightlessThreshold = weightlessThreshold;
            }
        }

        /************************************************************************************************************************/
    }
}