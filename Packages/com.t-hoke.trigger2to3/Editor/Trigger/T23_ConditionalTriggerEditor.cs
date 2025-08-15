#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ConditionalTrigger))]
    internal class T23_ConditionalTriggerEditor : T23_TriggerEditorBase
    {
        T23_ConditionalTrigger body;

        public enum CompParameterType
        {
            Constant = 0,
            PropertyBox = 1,
            DifferenceFromBefore = 2
        }

        private string[] CompOperator_a = { "Equal (=)", "Not Equal (!=)", "Greater (>)", "Less (<)", "Greater or Equal (>=)", "Less or Equal (<=)" };
        private string[] CompOperator_b = { "Equal (=)", "Not Equal (!=)" };

        protected override void DrawFields()
        {
            body = target as T23_ConditionalTrigger;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("passive"));
            if (!body.passive)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("allowContinuity"));
            }

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Base", EditorStyles.boldLabel);
            serializedObject.FindProperty("basePropertyBox").objectReferenceValue = EditorGUILayout.ObjectField("PropertyBox", body.basePropertyBox, typeof(T23_PropertyBox), true);

            if (body.basePropertyBox)
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Comparison", EditorStyles.boldLabel);
                serializedObject.FindProperty("compOperator").intValue = EditorGUILayout.Popup("Operator", body.compOperator, (body.basePropertyBox.valueType == 1 || body.basePropertyBox.valueType == 2) ? CompOperator_a : CompOperator_b);
                serializedObject.FindProperty("compParameterType").intValue = (int)(CompParameterType)EditorGUILayout.EnumPopup("Parameter Type", (CompParameterType)body.compParameterType);
                if (body.compParameterType == 0)
                {
                    switch (body.basePropertyBox.valueType)
                    {
                        case 0:
                            serializedObject.FindProperty("comp_b").boolValue = EditorGUILayout.Toggle("Value", body.comp_b);
                            break;
                        case 1:
                            serializedObject.FindProperty("comp_i").intValue = EditorGUILayout.IntField("Value", body.comp_i);
                            break;
                        case 2:
                            serializedObject.FindProperty("comp_f").floatValue = EditorGUILayout.FloatField("Value", body.comp_f);
                            break;
                        case 4:
                            serializedObject.FindProperty("comp_s").stringValue = EditorGUILayout.TextField("Value", body.comp_s);
                            break;
                    }
                }
                if (body.compParameterType == 1)
                {
                    serializedObject.FindProperty("compPropertyBox").objectReferenceValue = EditorGUILayout.ObjectField("PropertyBox", body.compPropertyBox, typeof(T23_PropertyBox), true);
                    if (body.compPropertyBox)
                    {
                        if (body.compPropertyBox.valueType != body.basePropertyBox.valueType)
                        {
                            EditorGUILayout.HelpBox(T23_Localization.GetWord("Action_propertybox"), MessageType.Error);
                        }
                    }
                }
            }
        }
    }
}
#endif
