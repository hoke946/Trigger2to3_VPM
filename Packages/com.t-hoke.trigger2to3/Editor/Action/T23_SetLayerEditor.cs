
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetLayer))]
    internal class T23_SetLayerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            var layer_prop = serializedObject.FindProperty("layer");
            layer_prop.intValue = EditorGUILayout.LayerField("Layer", layer_prop.intValue);
        }
    }
}
