using Animancer;
using Core.Lib;
using UnityEngine;


public class AnimancerPlayTransitionsOnEnable : MonoBehaviour
{
    [SerializeField] private int _layer;
    [SerializeField] private ClipTransition[] _clips;
    [SerializeField] private ClipTransition[] _loop;
    [SerializeField] private AnimancerComponent _animancer;

    private void OnValidate() => _animancer = _animancer ? _animancer : GetComponentInParent<AnimancerComponent>();

    [MyButton]
    private void OnEnable()
    {
        for (int i = 0; i < _clips.Length - 1; i++)
        {
            var index = i + 1;
            _clips[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_clips[index]);

            if (index == _clips.Length - 1 && _loop.Length != 0)
                _clips[index].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[0]);
        }

        for (int i = 0; i < _loop.Length - 1; i++)
        {
            var index = i + 1;
            _loop[i].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[index]);

            if (index == _loop.Length - 1)
                _loop[index].Events.OnEnd = () => _animancer.Layers[_layer].Play(_loop[0]);
        }
        
        _animancer.Layers[_layer].Play(_clips.Length != 0 ? _clips[0] : _loop[0]).Time = 0;
    }
}