
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetGameObjectActive))]
    internal class T23_SetGameObjectActiveEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawToggleOperationField();
        }
    }

}
