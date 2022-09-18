
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_TakeOwnership))]
    internal class T23_TakeOwnershipEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
        }
    }
}
