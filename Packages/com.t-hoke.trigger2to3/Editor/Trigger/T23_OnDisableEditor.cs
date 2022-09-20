#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnDisable))]
    internal class T23_OnDisableEditor : T23_TriggerEditorBase
    {
    }
}
#endif
