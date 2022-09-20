#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetAvatarUse))]
    internal class T23_SetAvatarUseEditor : T23_ActionEditorBase
    {
    }
}
#endif
