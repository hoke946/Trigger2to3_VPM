#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UdonSharp;
using System.Collections.Generic;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_Master))]
    internal class T23_MasterEditor : Editor
    {
        private ReorderableList broadcastReorderableList;
        private ReorderableList triggerReorderableList;
        private ReorderableList actionReorderableList;

        private UdonSharpProgramAsset setBroadcast;
        private UdonSharpProgramAsset addTrigger;
        private UdonSharpProgramAsset addAction;

        T23_Master master;

        private static T23_Master _copyTarget;

        void OnEnable()
        {
            master = target as T23_Master;
            if (master != null)
            {
                master.SetupGroup();
            }
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            master.CheckComponents();

            serializedObject.Update();

            T23_EditorUtility.ShowTitle("Master");

            GUILayout.Box("Group #" + master.groupID.ToString(), T23_EditorUtility.HeadlineStyle(true));

            GUILayout.Space(10);

            Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(master.gameObject);
            if (prefab != null)
            {
                EditorGUILayout.HelpBox(T23_Localization.GetWord("Master_prefab"), MessageType.Info);
            }

            SerializedProperty broadcastProp = serializedObject.FindProperty("broadcastTitles");
            if (broadcastReorderableList == null)
            {
                broadcastReorderableList = new ReorderableList(serializedObject, broadcastProp);
                broadcastReorderableList.draggable = prefab == null;
                broadcastReorderableList.onCanAddCallback += (list) => { return prefab == null; };
                broadcastReorderableList.onCanRemoveCallback += (list) => { return prefab == null; };
                broadcastReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Broadcast");
                broadcastReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(rect, master.broadcastTitles[index]);
                };
                broadcastReorderableList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    var moduleList = T23_EditorUtility.GetModuleClasses(typeof(T23_BroadcastBase), false);
                    foreach (var pair in moduleList)
                    {
                        menu.AddItem(new GUIContent(pair.Key), false, () =>
                        {
                            master.SetBroadcast(pair.Value);
                        });
                    }
                    menu.DropDown(buttonRect);
                };
                broadcastReorderableList.onChangedCallback = ChangeBroadcast;
            }
            broadcastReorderableList.DoLayoutList();

            GUILayout.Space(10);

            SerializedProperty triggerProp = serializedObject.FindProperty("triggerTitles");
            if (triggerReorderableList == null)
            {
                triggerReorderableList = new ReorderableList(serializedObject, triggerProp);
                triggerReorderableList.draggable = prefab == null;
                triggerReorderableList.onCanAddCallback += (list) => { return prefab == null; };
                triggerReorderableList.onCanRemoveCallback += (list) => { return prefab == null; };
                triggerReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Trigger");
                triggerReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(rect, master.triggerTitles[index]);
                };
                triggerReorderableList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    var moduleList = T23_EditorUtility.GetModuleClasses(typeof(T23_TriggerBase), true);
                    foreach (var pair in moduleList)
                    {
                        menu.AddItem(new GUIContent(pair.Key), false, () =>
                        {
                            master.AddTrigger(pair.Value);
                        });
                    }
                    menu.DropDown(buttonRect);
                };
                triggerReorderableList.onChangedCallback = ChangeTrigger;
            }
            triggerReorderableList.DoLayoutList();

            GUILayout.Space(10);

            SerializedProperty actionProp = serializedObject.FindProperty("actionTitles");
            if (actionReorderableList == null)
            {
                actionReorderableList = new ReorderableList(serializedObject, actionProp);
                actionReorderableList.draggable = prefab == null;
                actionReorderableList.onCanAddCallback += (list) => { return prefab == null; };
                actionReorderableList.onCanRemoveCallback += (list) => { return prefab == null; };
                actionReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Action");
                actionReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(rect, master.actionTitles[index]);
                };
                actionReorderableList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    var moduleList = T23_EditorUtility.GetModuleClasses(typeof(T23_ActionBase), true);
                    foreach (var pair in moduleList)
                    {
                        menu.AddItem(new GUIContent(pair.Key), false, () =>
                        {
                            master.AddAction(pair.Value);
                        });
                    }
                    menu.DropDown(buttonRect);
                };
                actionReorderableList.onChangedCallback = ChangeAction;
            }
            actionReorderableList.DoLayoutList();

            GUILayout.Space(10);

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(master.hasObjectSync);
            serializedObject.FindProperty("reliable").boolValue = EditorGUILayout.Toggle("SyncMethod Manual", master.reliable);
            EditorGUI.EndDisabledGroup();
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                master.OrderComponents();
                master.UnifyUdonParameters();
                serializedObject.Update();
            }
            if (master.hasObjectSync)
            {
                EditorGUILayout.HelpBox(T23_Localization.GetWord("Master_objectsync"), MessageType.Info);
            }

            EditorGUILayout.BeginHorizontal();
            Color oldBackgroundColor = GUI.backgroundColor;
            if (_copyTarget != null && _copyTarget == master) { GUI.backgroundColor = Color.green; }
            if (GUILayout.Button("Set Copy Target"))
            {
                if (_copyTarget == master)
                {
                    _copyTarget = null;
                }
                else
                {
                    _copyTarget = master;
                }
            }
            GUI.backgroundColor = oldBackgroundColor;
            EditorGUI.BeginDisabledGroup(_copyTarget == null);
            if (GUILayout.Button("Copy Modules"))
            {
                CopyModules();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            if (master.shouldMoveComponents)
            {
                string msg = "";
                if (prefab == null)
                {
                    if (master.MoveComponents())
                    {
                        master.shouldMoveComponents = false;
                    }
                    else
                    {
                        msg = "Master_align";
                    }
                }
                else
                {
                    master.shouldMoveComponents = false;
                }
                if (msg != "")
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox(T23_Localization.GetWord(msg), MessageType.Warning);
                    if (GUILayout.Button("Clear"))
                    {
                        master.shouldMoveComponents = false;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            serializedObject.ApplyModifiedProperties();

        }

        private void ChangeBroadcast(ReorderableList list)
        {
            EditorApplication.delayCall += () => master.ChangeBroadcast();
        }

        private void ChangeTrigger(ReorderableList list)
        {
            EditorApplication.delayCall += () => master.ChangeTrigger();
        }

        private void ChangeAction(ReorderableList list)
        {
            EditorApplication.delayCall += () => master.ChangeAction();
        }

        private void CopyModules()
        {
            if (_copyTarget == null || _copyTarget == master) { return; }

            EditorApplication.delayCall += () => CopyModules_delay();
        }

        private void CopyModules_delay()
        {
            master.ClearComponents();

            if (_copyTarget.broadcastSet.module != null)
            {
                master.SetBroadcast(_copyTarget.broadcastSet.module.GetType());
                CopyComponentValues(_copyTarget.broadcastSet, master.broadcastSet);
            }
            foreach (var trigger in _copyTarget.triggerSet)
            {
                if (trigger.module != null)
                {
                    master.AddTrigger(trigger.module.GetType());
                    CopyComponentValues(trigger, master.triggerSet[master.triggerSet.Count - 1]);
                }
            }
            foreach (var action in _copyTarget.actionSet)
            {
                if (action.module != null)
                {
                    master.AddAction(action.module.GetType());
                    CopyComponentValues(action, master.actionSet[master.actionSet.Count - 1]);
                }
            }
        }

        private void CopyComponentValues(T23_Master.ComponentSet baseComponent, T23_Master.ComponentSet targetComponent)
        {
            int groupID = targetComponent.module.groupID;
            int priority = targetComponent.module.priority;
            ComponentUtility.CopyComponent(baseComponent.module);
            ComponentUtility.PasteComponentValues(targetComponent.module);
            targetComponent.module.groupID = groupID;
            targetComponent.module.priority = priority;
            targetComponent.module.title = targetComponent.title = baseComponent.title;
        }
    }
}
#endif
