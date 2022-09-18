
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetPropertyBox))]
    internal class T23_SetPropertyBoxEditor : T23_ActionEditorBase
    {
        private string[] CalcOperator_a = { "Update", "Substitute (=)", "Add (+)", "Subtract (-)", "Multiple (*)", "Divide" };
        private string[] CalcOperator_b = { "Update", "Substitute (=)" };

        protected override void DrawFields()
        {
            var body = target as T23_SetPropertyBox;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("propertyBox"));
            if (body.propertyBox)
            {
                serializedObject.FindProperty("calcOperator").intValue = EditorGUILayout.Popup("Operator", body.calcOperator, (body.propertyBox.valueType == 1 || body.propertyBox.valueType == 2 || body.propertyBox.valueType == 3) ? CalcOperator_a : CalcOperator_b);
                if (body.calcOperator != 0)
                {
                    if (body.propertyBox.valueType == 0)
                    {
                        PropertyBoxField("value_bool", "valuePropertyBox", "usePropertyBox", () => serializedObject.FindProperty("value_bool").boolValue = EditorGUILayout.Toggle("Value_bool", body.value_bool));
                    }
                    else if (body.propertyBox.valueType == 1)
                    {
                        PropertyBoxField("value_int", "valuePropertyBox", "usePropertyBox", () => serializedObject.FindProperty("value_int").intValue = EditorGUILayout.IntField("Value_int", body.value_int));
                    }
                    else if (body.propertyBox.valueType == 2 || (body.propertyBox.valueType == 3 && (body.calcOperator == 4 || body.calcOperator == 5)))
                    {
                        PropertyBoxField("value_float", "valuePropertyBox", "usePropertyBox", () => serializedObject.FindProperty("value_float").floatValue = EditorGUILayout.FloatField("Value_float", body.value_float));
                    }
                    else if (body.propertyBox.valueType == 3)
                    {
                        PropertyBoxField("value_Vector3", "valuePropertyBox", "usePropertyBox", () => serializedObject.FindProperty("value_Vector3").vector3Value = EditorGUILayout.Vector3Field("Value_Vector3", body.value_Vector3));
                    }
                    else if (body.propertyBox.valueType == 4)
                    {
                        PropertyBoxField("value_string", "valuePropertyBox", "usePropertyBox", () => serializedObject.FindProperty("value_string").stringValue = EditorGUILayout.TextField("Value_string", body.value_string));
                    }
                }
            }
        }
    }
}
