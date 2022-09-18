
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_PickupDrop))]
    internal class T23_PickupDropEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
        }
    }
}
