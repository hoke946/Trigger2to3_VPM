#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnDrop))]
    internal class T23_OnDropEditor : T23_TriggerEditorBase
    {
    }
}
#endif
