#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UdonSharp;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VRC.Udon;
using System;
using System.Collections;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_UIOnEndEdit))]
    internal class T23_UIOnEndEditEditor : T23_TriggerEditorBase
    {
        private int _eventIndex;

        protected override void DrawFields()
        {
            var body = target as T23_UIOnEndEdit;
            var component = serializedObject.FindProperty("inputField");
            GUI.enabled = false;
            EditorGUILayout.PropertyField(component);
            GUI.enabled = true;

            if (component.objectReferenceValue == null)
            {
                var ipt = body_base.GetComponent<InputField>();
                if (ipt != null)
                {
                    component.objectReferenceValue = ipt;
                }
            }

            var inputField = (InputField)component.objectReferenceValue;
            if (inputField != null)
            {
                _eventIndex = -1;
                for (int i = 0; i < inputField.onEndEdit.GetPersistentEventCount(); i++)
                {
                    var ev = inputField.onEndEdit.GetPersistentTarget(i);
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
            else
            {
                EditorGUILayout.HelpBox("対象のUIコンポーネントが存在しません", MessageType.Error);
            }
        }

        void OnDestroy()
        {
            var body = target as T23_UIOnEndEdit;
            if (body == null)
            {
                RemoveEvent();
            }
        }

        private void AddEvent()
        {
            var body = target as T23_UIOnEndEdit;
            if (body.inputField != null)
            {
                var udon = UdonSharpEditorUtility.GetBackingUdonBehaviour(body);
                var targetinfo = UnityEvent.GetValidMethodInfo(udon, "SendCustomEvent", new Type[] { typeof(string) });
                Undo.RecordObject(body.inputField, "Add Event");
                var action = Delegate.CreateDelegate(typeof(UnityAction<string>), udon, targetinfo, false) as UnityAction<string>;
                UnityEditor.Events.UnityEventTools.AddStringPersistentListener(body.inputField.onEndEdit, action, "Trigger");
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.inputField);
                EditorUtility.SetDirty(body.inputField.gameObject);
            }
        }

        private void RemoveEvent()
        {
            var body = target as T23_UIOnEndEdit;
            if (body.inputField != null && _eventIndex != -1)
            {
                Undo.RecordObject(body.inputField, "Remove Event");
                UnityEditor.Events.UnityEventTools.RemovePersistentListener(body.inputField.onEndEdit, _eventIndex);
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.inputField);
            }
        }
    }
}
#endif
