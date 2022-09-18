
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ActiveConditionalTrigger))]
    internal class T23_ActiveConditionalTriggerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
        }
    }
}
