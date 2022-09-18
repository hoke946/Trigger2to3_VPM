
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AudioTrigger))]
    internal class T23_AudioTriggerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
        }
    }
}
