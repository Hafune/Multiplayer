using System.Runtime.CompilerServices;
using Reflex;
using UnityEngine;

#if UNITY_EDITOR
using Reflex.Injectors;
using UnityEditor;
#endif

namespace Lib
{
    public abstract class MonoConstruct : MonoBehaviour
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetupContext(Context c, MonoConstruct monoConstruct) => monoConstruct.context = c;

#if UNITY_EDITOR
        private Context _context;

        protected Context context
        {
            get
            {
                if (_context is null && Application.isPlaying)
                    Debug.LogWarning($"Context был получен из Editor: {nameof(EditorRuntimeContextAccess)}", this);

                return _context ??= EditorRuntimeContextAccess.Context ?? EditorNonRuntimeContextAccess.Context;
            }
            private set => _context = value;
        }

#else
        protected Context context;
#endif
    }
}