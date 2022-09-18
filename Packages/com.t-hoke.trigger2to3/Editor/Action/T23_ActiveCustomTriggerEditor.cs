
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ActiveCustomTrigger))]
    internal class T23_ActiveCustomTriggerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();

            /*
            List<string> customNameList = new List<string>();
            if (body.recievers != null)
            {
                foreach (var go in body.recievers)
                {
                    if (go)
                    {
                        customNameList.AddRange(GetCustomNameList(go));
                    }
                }
            }
            if (customNameList.Count > 0)
            {
                var index = EditorGUILayout.Popup("Name", customNameList.IndexOf(body.Name), customNameList.ToArray());
                serializedObject.FindProperty("Name").stringValue = index >= 0 ? customNameList[index] : "";
            }
            else
            */
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"));
            }
            if (!master || master.randomize)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("randomAvg"));
            }
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
