using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ScaledByExplosionScaleValue : IValueChangeListener
    {
        [SerializeField] public Transform[] transforms;

        public void UpdateByValue(float value)
        {
            for (int i = 0, iMax = transforms.Length; i < iMax; i++)
                transforms[i].localScale = new Vector3(value, value, value);
        }
    }
}