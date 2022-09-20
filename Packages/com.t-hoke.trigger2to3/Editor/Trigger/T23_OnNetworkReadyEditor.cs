#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnNetworkReady))]
    internal class T23_OnNetworkReadyEditor : T23_TriggerEditorBase
    {
    }
}
#endif
