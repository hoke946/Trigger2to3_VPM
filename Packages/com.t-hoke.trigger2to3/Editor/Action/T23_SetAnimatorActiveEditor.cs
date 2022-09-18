
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetAnimatorActive))]
    internal class T23_SetAnimatorActiveEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawToggleOperationField();
        }
    }
}
