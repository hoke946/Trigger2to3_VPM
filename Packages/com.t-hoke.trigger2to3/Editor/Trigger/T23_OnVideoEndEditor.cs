#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnVideoEnd))]
    internal class T23_OnVideoEndEditor : T23_TriggerEditorBase
    {
    }
}
#endif
