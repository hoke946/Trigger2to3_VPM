#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UdonSharp;
using VRC.Udon;
using UnityEditor;
using UdonSharpEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnInteract))]
    internal class T23_OnInteractEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUI.BeginChangeCheck();
            UdonSharpGUI.DrawInteractSettings(target);
            if (EditorGUI.EndChangeCheck())
            {
                if (master)
                {
                    UdonBehaviour behaviour = UdonSharpEditorUtility.GetBackingUdonBehaviour((UdonSharpBehaviour)target);
                    master.interactText = behaviour.interactText;
                    master.proximity = behaviour.proximity;
                    master.OrderComponents();
                    master.UnifyUdonParameters();
                }
            }
        }
    }
}
#endif
