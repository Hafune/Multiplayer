using UnityEditor;

namespace Core.Lib
{
    public static class MyEditorUtility
    {
#if UNITY_EDITOR
        public static bool IsUnsafeEditorCall() => EditorApplication.isPlaying ||
                                                   EditorApplication.isPaused ||
                                                   EditorApplication.isCompiling ||
                                                   EditorApplication.isPlayingOrWillChangePlaymode;
#endif
    }
}