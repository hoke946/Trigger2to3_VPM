#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UdonSharp;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VRC.Udon;
using System.Collections;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_UIOnValueChanged))]
    internal class T23_UIOnValueChangedEditor : T23_TriggerEditorBase
    {
        private int _eventIndex;
        private System.Type _componentType;

        enum IsOnValue
        {
            Any = 0,
            On = 1,
            Off = 2
        }

        protected override void DrawFields()
        {
            T23_UIOnValueChanged body = target as T23_UIOnValueChanged;
            var component = serializedObject.FindProperty("UIComponent");
            var componentType = serializedObject.FindProperty("componentType");
            GUI.enabled = false;
            EditorGUILayout.PropertyField(component);
            GUI.enabled = true;

            if (component.objectReferenceValue == null)
            {
                componentType.stringValue = "";
                System.Type[] types = { typeof(Toggle), typeof(InputField), typeof(Dropdown), typeof(InputField), typeof(Slider), typeof(Scrollbar) };
                for (int i = 0; i < types.Length; i++)
                {
                    var getcom = body_base.GetComponent(types[i]);
                    if (getcom != null)
                    {
                        serializedObject.FindProperty("UIComponent").objectReferenceValue = getcom;
                        serializedObject.FindProperty("componentType").stringValue = types[i].ToString();
                        serializedObject.ApplyModifiedProperties();
                        break;
                    }
                }
            }

            if (component.objectReferenceValue != null)
            {
                var actionEvent = GetEvent();
                if (actionEvent != null)
                {
                    _eventIndex = -1;
                    for (int i = 0; i < actionEvent.GetPersistentEventCount(); i++)
                    {
                        var ev = actionEvent.GetPersistentTarget(i);
                        if (ev == UdonSharpEditorUtility.GetBackingUdonBehaviour(body))
                        {
                            _eventIndex = i;
                        }
                    }
                    if (_eventIndex == -1)
                    {
                        AddEvent();
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("対象のUIコンポーネントが存在しません", MessageType.Error);
            }

            if (body.componentType == typeof(Toggle).ToString())
            {
                DrawToggleFields();
            }
            if (body.componentType == typeof(Dropdown).ToString())
            {
                DrawDropdownFields();
            }
        }

        private UnityEventBase GetEvent()
        {
            T23_UIOnValueChanged body = target as T23_UIOnValueChanged;
            if (body.componentType == typeof(Toggle).ToString())
            {
                var toggle = (Toggle)body.UIComponent;
                return toggle.onValueChanged;
            }
            if (body.componentType == typeof(InputField).ToString())
            {
                var inputField = (InputField)body.UIComponent;
                return inputField.onValueChanged;
            }
            if (body.componentType == typeof(Dropdown).ToString())
            {
                var dropdown = (Dropdown)body.UIComponent;
                return dropdown.onValueChanged;
            }
            if (body.componentType == typeof(Slider).ToString())
            {
                var slider = (Slider)body.UIComponent;
                return slider.onValueChanged;
            }
            if (body.componentType == typeof(Scrollbar).ToString())
            {
                var scrollbar = (Scrollbar)body.UIComponent;
                return scrollbar.onValueChanged;
            }
            return null;
        }

        private void DrawToggleFields()
        {
            var any = serializedObject.FindProperty("any");
            var isOn = serializedObject.FindProperty("isOn");
            IsOnValue value = any.boolValue ? IsOnValue.Any : isOn.boolValue ? IsOnValue.On : IsOnValue.Off;
            value = (IsOnValue)EditorGUILayout.EnumPopup("Value", value);
            any.boolValue = value == IsOnValue.Any;
            isOn.boolValue = value == IsOnValue.On;
        }

        private void DrawDropdownFields()
        {
            var any = serializedObject.FindProperty("any");
            var value = serializedObject.FindProperty("value");
            EditorGUILayout.PropertyField(any);
            EditorGUI.BeginDisabledGroup(any.boolValue);
            EditorGUILayout.PropertyField(value);
            EditorGUI.EndDisabledGroup();
        }

        void OnDestroy()
        {
            T23_UIOnValueChanged body = target as T23_UIOnValueChanged;
            if (body == null)
            {
                RemoveEvent();
            }
        }

        private void AddEvent()
        {
            T23_UIOnValueChanged body = target as T23_UIOnValueChanged;
            if (body.UIComponent != null)
            {
                var udon = UdonSharpEditorUtility.GetBackingUdonBehaviour(body);
                var targetinfo = UnityEvent.GetValidMethodInfo(udon, "SendCustomEvent", new System.Type[] { typeof(string) });
                Undo.RecordObject(body.UIComponent, "Add Event");
                var action = System.Delegate.CreateDelegate(typeof(UnityAction<string>), udon, targetinfo, false) as UnityAction<string>;
                UnityEditor.Events.UnityEventTools.AddStringPersistentListener(GetEvent(), action, "OnValueChanged");
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.UIComponent);
                EditorUtility.SetDirty(body.gameObject);
            }
        }

        private void RemoveEvent()
        {
            T23_UIOnValueChanged body = target as T23_UIOnValueChanged;
            if (body.UIComponent != null && _eventIndex != -1)
            {
                Undo.RecordObject(body.UIComponent, "Remove Event");
                UnityEditor.Events.UnityEventTools.RemovePersistentListener(GetEvent(), _eventIndex);
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.UIComponent);
            }
        }
    }
}
#endif
