
using UnityEditor;
using UnityEngine;

namespace Trigger2to3
{
    [InitializeOnLoad]
    public class T23_AutoUpdateCommonBuffers
    {
        static T23_AutoUpdateCommonBuffers()
        {
            EditorApplication.hierarchyWindowItemOnGUI += delegate (int instanceID, Rect selectionRect)
            {
                if (Event.current.commandName == "Duplicate" || Event.current.commandName == "SoftDelete" || Event.current.commandName == "UndoRedoPerformed")
                {
                    T23_EditorUtility.UpdateAllCommonBuffersRelate();
                }
            };
        }
    }
}
