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
    [CustomEditor(typeof(T23_UIOnClickButton))]
    internal class T23_UIOnClickButtonEditor : T23_TriggerEditorBase
    {
        private int _eventIndex;

        protected override void DrawFields()
        {
            var body = target as T23_UIOnClickButton;
            var component = serializedObject.FindProperty("button");
            GUI.enabled = false;
            EditorGUILayout.PropertyField(component);
            GUI.enabled = true;

            if (component.objectReferenceValue == null)
            {
                var btn = body_base.GetComponent<Button>();
                if (btn != null)
                {
                    component.objectReferenceValue = btn;
                }
            }

            var button = (Button)component.objectReferenceValue;
            if (button != null)
            {
                _eventIndex = -1;
                for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++)
                {
                    var ev = button.onClick.GetPersistentTarget(i);
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
            var body = target as T23_UIOnClickButton;
            if (body == null)
            {
                RemoveEvent();
            }
        }

        private void AddEvent()
        {
            var body = target as T23_UIOnClickButton;
            if (body.button != null)
            {
                var udon = UdonSharpEditorUtility.GetBackingUdonBehaviour(body);
                var targetinfo = UnityEvent.GetValidMethodInfo(udon, "SendCustomEvent", new Type[] { typeof(string) });
                Undo.RecordObject(body.button, "Add Event");
                var action = Delegate.CreateDelegate(typeof(UnityAction<string>), udon, targetinfo, false) as UnityAction<string>;
                UnityEditor.Events.UnityEventTools.AddStringPersistentListener(body.button.onClick, action, "Trigger");
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.button);
                EditorUtility.SetDirty(body.button.gameObject);
            }
        }

        private void RemoveEvent()
        {
            var body = target as T23_UIOnClickButton;
            if (body.button != null && _eventIndex != -1)
            {
                Undo.RecordObject(body.button, "Remove Event");
                UnityEditor.Events.UnityEventTools.RemovePersistentListener(body.button.onClick, _eventIndex);
                PrefabUtility.RecordPrefabInstancePropertyModifications(body.button);
            }
        }
    }
}
#endif
