
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_DestroyObject))]
    internal class T23_DestroyObjectEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
        }
    }
}
