using Core.Lib;
using UnityEngine;

namespace Core.Components
{
    [DisallowMultipleComponent, RequireComponent(typeof(MultiplayerChanges))]
    public class MultiplayerChangesProvider : MonoProvider<MultiplayerChangesComponent>
    {
    
    }
}