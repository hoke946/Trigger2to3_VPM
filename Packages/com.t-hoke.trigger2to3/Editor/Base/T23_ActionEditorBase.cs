#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UdonSharp;
using VRC.Udon;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UdonSharpEditor;

namespace Trigger2to3
{
    public class T23_ActionEditorBase : T23_ModuleEditorBase
    {
        public enum ToggleOperation
        {
            True = 0,
            False = 1,
            Toggle = 2
        }

        public enum BoolOperation
        {
            True = 1,
            False = 0
        }

        protected override void DrawActionHeadder()
        {
            if (master)
            {
                T23_EditorUtility.ShowSwapButton(master, body_base.title);
                body_base.priority = master.actionTitles.IndexOf(body_base.title);
            }
            else
            {
                body_base.priority = EditorGUILayout.IntField("Priority", body_base.priority);
            }
        }

        protected override void DrawCommonFields()
        {
            if (!master || master.randomize)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("randomAvg"));
            }
        }

        protected void DrawRecieversList(string fieldName = "recievers")
        {
            SerializedProperty recieverProp = serializedObject.FindProperty(fieldName);
            if (Application.unityVersion.Substring(0, 4) == "2019")
            {
                if (recieverReorderableList == null)
                {
                    recieverReorderableList = new ReorderableList(serializedObject, recieverProp);
                    recieverReorderableList.draggable = true;
                    recieverReorderableList.displayAdd = true;
                    recieverReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, T23_EditorUtility.ToUnityFieldName(fieldName));
                    recieverReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        rect.height = EditorGUIUtility.singleLineHeight;
                        EditorGUI.PropertyField(rect, recieverProp.GetArrayElementAtIndex(index), new GUIContent(""));
                    };
                }

                recieverReorderableList.DoLayoutList();
            }
            else
            {
                EditorGUILayout.PropertyField(recieverProp, new GUIContent(fieldName));
            }
        }

        protected void DrawToggleOperationField()
        {
            var toggle_prop = serializedObject.FindProperty("toggle");
            var operation_prop = serializedObject.FindProperty("operation");
            PropertyBoxField("operation", "propertyBox", "usePropertyBox", () => ToggleOperationField(toggle_prop, operation_prop));
        }

        private void ToggleOperationField(SerializedProperty toggle_prop, SerializedProperty operation_prop)
        {
            EditorGUI.BeginChangeCheck();
            ToggleOperation operation = (ToggleOperation)EditorGUILayout.EnumPopup("Operation", GetToggleOperation(toggle_prop, operation_prop));
            if (EditorGUI.EndChangeCheck())
            {
                switch (operation)
                {
                    case ToggleOperation.True:
                        toggle_prop.boolValue = false;
                        operation_prop.boolValue = true;
                        break;
                    case ToggleOperation.False:
                        toggle_prop.boolValue = false;
                        operation_prop.boolValue = false;
                        break;
                    case ToggleOperation.Toggle:
                        toggle_prop.boolValue = true;
                        break;
                }
            }
        }

        private ToggleOperation GetToggleOperation(SerializedProperty toggle_prop, SerializedProperty operation_prop)
        {
            if (toggle_prop.boolValue)
            {
                return ToggleOperation.Toggle;
            }
            else
            {
                if (operation_prop.boolValue)
                {
                    return ToggleOperation.True;
                }
                else
                {
                    return ToggleOperation.False;
                }
            }
        }

        protected void DrawBoolOperationField()
        {
            PropertyBoxField("operation", "propertyBox", "usePropertyBox", () => serializedObject.FindProperty("operation").boolValue = (BoolOperation)EditorGUILayout.EnumPopup("Operation", (BoolOperation)System.Convert.ToInt32(serializedObject.FindProperty("operation").boolValue)) == BoolOperation.True);
        }

        protected void PropertyBoxField(string constFieldName, string propertyBoxFieldName, string switchFieldName, Action edit = null)
        {
            EditorGUILayout.BeginHorizontal();
            var switchField = serializedObject.FindProperty(switchFieldName);
            if (switchField.boolValue)
            {
                var propertyBoxField = serializedObject.FindProperty(propertyBoxFieldName);
                propertyBoxField.objectReferenceValue = EditorGUILayout.ObjectField(T23_EditorUtility.ToUnityFieldName(constFieldName), propertyBoxField.objectReferenceValue, typeof(T23_PropertyBox), true);
            }
            else
            {
                if (edit == null)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(constFieldName));
                }
                else
                {
                    edit();
                }
            }
            //EditorGUILayout.BeginHorizontal();
            //GUILayout.Space(EditorGUIUtility.currentViewWidth - 150);
            string buttonLabel = switchField.boolValue ? "to Constant" : "to PropertyBox";
            var buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 10;
            buttonStyle.stretchWidth = false;
            Color oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button(buttonLabel, buttonStyle))
            {
                switchField.boolValue = !switchField.boolValue;
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            GUI.backgroundColor = oldBackgroundColor;
            EditorGUILayout.EndHorizontal();
            if (switchField.boolValue)
            {
                var propertyBoxField = serializedObject.FindProperty(propertyBoxFieldName);
                if (propertyBoxField.objectReferenceValue)
                {
                    var propertyBox = (T23_PropertyBox)propertyBoxField.objectReferenceValue;
                    var constField = serializedObject.FindProperty(constFieldName);
                    bool unsuitable = false;
                    if (constField.propertyType != SerializedPropertyType.String)
                    {
                        if (propertyBox.valueType == 0 && constField.propertyType != SerializedPropertyType.Boolean) { unsuitable = true; }
                        if (propertyBox.valueType == 1 && constField.propertyType != SerializedPropertyType.Integer) { unsuitable = true; }
                        if (propertyBox.valueType == 2 && constField.propertyType != SerializedPropertyType.Float) { unsuitable = true; }
                        if (propertyBox.valueType == 3 && constField.propertyType != SerializedPropertyType.Vector3) { unsuitable = true; }
                        if (propertyBox.valueType == 4 && constField.propertyType != SerializedPropertyType.String) { unsuitable = true; }
                    }
                    if (unsuitable)
                    {
                        EditorGUILayout.HelpBox("PropertyBox の ValueType が不適合です", MessageType.Error);
                    }
                }
            }
        }
    }
}
#endif
