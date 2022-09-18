
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AudioBank))]
    internal class T23_AudioBankEditor : Editor
    {
        T23_AudioBank body;

        SerializedProperty prop;

        private ReorderableList recieverReorderableList;

        public enum Order
        {
            InOrder = 0,
            InOrderReversing = 1,
            Shuffle = 2,
            Random = 3
        }

        public enum Style
        {
            OneShot = 0,
            Continuous = 1
        }

        void OnEnable()
        {
            body = target as T23_AudioBank;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            T23_EditorUtility.ShowTitle("Option");

            GUILayout.Box("AudioBank", T23_EditorUtility.HeadlineStyle());

            prop = serializedObject.FindProperty("source");
            EditorGUILayout.PropertyField(prop);
            serializedObject.FindProperty("playbackOrder").intValue = (int)(Order)EditorGUILayout.EnumPopup("Playback Order", (Order)body.playbackOrder);
            serializedObject.FindProperty("playbackStyle").intValue = (int)(Style)EditorGUILayout.EnumPopup("Playback Style", (Style)body.playbackStyle);
            prop = serializedObject.FindProperty("repeat");
            EditorGUILayout.PropertyField(prop);
            prop = serializedObject.FindProperty("minPitchRange");
            EditorGUILayout.PropertyField(prop);
            prop = serializedObject.FindProperty("maxPitchRange");
            EditorGUILayout.PropertyField(prop);

            EditorGUILayout.BeginHorizontal();
            prop = serializedObject.FindProperty("onPlay");
            EditorGUILayout.PropertyField(prop);
            /*
            List<string> onPlayCustomNameList = new List<string>();
            if (body.onPlay)
            {
                onPlayCustomNameList = GetCustomNameList(body.onPlay);
            }
            if (onPlayCustomNameList.Count > 0)
            {
                var index = EditorGUILayout.Popup(onPlayCustomNameList.IndexOf(body.onPlayCustomName), onPlayCustomNameList.ToArray());
                serializedObject.FindProperty("onPlayCustomName").stringValue = index >= 0 ? onPlayCustomNameList[index] : "";
            }
            else
            */
            {
                prop = serializedObject.FindProperty("onPlayCustomName");
                EditorGUILayout.PropertyField(prop, new GUIContent(""));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            prop = serializedObject.FindProperty("onStop");
            EditorGUILayout.PropertyField(prop);
            /*
            List<string> onStopCustomNameList = new List<string>();
            if (body.onStop)
            {
                onStopCustomNameList = GetCustomNameList(body.onStop);
            }
            if (onStopCustomNameList.Count > 0)
            {
                var index = EditorGUILayout.Popup(onStopCustomNameList.IndexOf(body.onStopCustomName), onStopCustomNameList.ToArray());
                serializedObject.FindProperty("onStopCustomName").stringValue = index >= 0 ? onStopCustomNameList[index] : "";
            }
            else
            */
            {
                prop = serializedObject.FindProperty("onStopCustomName");
                EditorGUILayout.PropertyField(prop, new GUIContent(""));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            prop = serializedObject.FindProperty("onChange");
            EditorGUILayout.PropertyField(prop);
            /*
            List<string> onChangeCustomNameList = new List<string>();
            if (body.onChange)
            {
                onChangeCustomNameList = GetCustomNameList(body.onChange);
            }
            if (onChangeCustomNameList.Count > 0)
            {
                var index = EditorGUILayout.Popup(onChangeCustomNameList.IndexOf(body.onChangeCustomName), onChangeCustomNameList.ToArray());
                serializedObject.FindProperty("onChangeCustomName").stringValue = index >= 0 ? onChangeCustomNameList[index] : "";
            }
            else
            */
            {
                prop = serializedObject.FindProperty("onChangeCustomName");
                EditorGUILayout.PropertyField(prop, new GUIContent(""));
            }
            EditorGUILayout.EndHorizontal();

            SerializedProperty recieverProp = serializedObject.FindProperty("clips");
            if (recieverReorderableList == null)
            {
                recieverReorderableList = new ReorderableList(serializedObject, recieverProp);
                recieverReorderableList.draggable = true;
                recieverReorderableList.displayAdd = true;
                recieverReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Clips");
                recieverReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    body.clips[index] = (AudioClip)EditorGUI.ObjectField(rect, body.clips[index], typeof(AudioClip), false);
                };
            }
            recieverReorderableList.DoLayoutList();

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
