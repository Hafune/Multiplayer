using Core.Lib;
using UnityEngine;

namespace Core.Components
{
    [DisallowMultipleComponent, RequireComponent(typeof(MultiplayerData))]
    public class MultiplayerDataProvider : MonoProvider<MultiplayerDataComponent>
    {
    
    }
}