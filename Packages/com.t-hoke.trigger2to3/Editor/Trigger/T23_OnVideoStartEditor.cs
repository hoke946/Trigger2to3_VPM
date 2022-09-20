#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnVideoStart))]
    internal class T23_OnVideoStartEditor : T23_TriggerEditorBase
    {
    }
}
#endif
