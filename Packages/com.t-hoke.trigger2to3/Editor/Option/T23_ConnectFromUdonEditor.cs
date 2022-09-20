#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ConnectFromUdon))]
    internal class T23_ConnectFromUdonEditor : Editor
    {
        T23_ConnectFromUdon body;

        SerializedProperty prop;

        void OnEnable()
        {
            body = target as T23_ConnectFromUdon;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            T23_EditorUtility.ShowTitle("Option");
            GUILayout.Box("ConnectFromUdon", T23_EditorUtility.HeadlineStyle());

            serializedObject.Update();

            prop = serializedObject.FindProperty("target");
            EditorGUILayout.PropertyField(prop);
            /*
            List<string> customNameList = new List<string>();
            if (body.target)
            {
                customNameList = GetCustomNameList(body.target);
                if (customNameList.Count == 0)
                {
                    EditorGUILayout.HelpBox("CustomTrigger が含まれていない、\nまたは、CustomTrigger Name が未記入です。", MessageType.Warning);
                }
            }
            if (customNameList.Count > 0)
            {
                var index = EditorGUILayout.Popup("Custom Trigger Name", customNameList.IndexOf(body.customTriggerName), customNameList.ToArray());
                serializedObject.FindProperty("customTriggerName").stringValue = index >= 0 ? customNameList[index] : "";
            }
            else
            */
            {
                prop = serializedObject.FindProperty("customTriggerName");
                EditorGUILayout.PropertyField(prop);
            }

            serializedObject.ApplyModifiedProperties();
        }

        /*
        private List<string> GetCustomNameList(GameObject targetObject)
        {
            List<string> list = new List<string>();
            var udons = targetObject.GetComponents<UdonBehaviour>();
            foreach (var udon in udons)
            {
                UdonSharpBehaviour usharp = UdonSharpEditorUtility.FindProxyBehaviour(udon);
                if (usharp && usharp.GetUdonSharpComponent<T23_CustomTrigger>())
                {
                    var nameField = usharp.GetProgramVariable("Name") as string;
                    if (nameField != null)
                    {
                        if (nameField != "" && !list.Contains(nameField))
                        {
                            list.Add(nameField);
                        }
                    }
                }
            }
            return list;
        }
        */
    }
}
#endif
