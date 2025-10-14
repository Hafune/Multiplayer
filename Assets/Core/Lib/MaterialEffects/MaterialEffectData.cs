using UnityEngine;

namespace Core.Lib
{
    [CreateAssetMenu(menuName = "Game Config/Effects/Materials/" + nameof(MaterialEffectData))]
    public class MaterialEffectData : ScriptableObject
    {
        [SerializeField] public Material material;
        [SerializeField] public float time;
    }
}