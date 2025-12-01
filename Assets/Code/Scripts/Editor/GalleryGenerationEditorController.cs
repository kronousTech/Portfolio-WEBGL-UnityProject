using KronosTech.Data;
using KronosTech.Gallery.Rooms;
using UnityEditor;
using UnityEngine;

public class GalleryGenerationEditorController : EditorWindow
{
    private RoomTagFlags _tags;

    [MenuItem("KronosTech/Gallery Generation")]
    public static void OpenWindow()
    {
        GetWindow<GalleryGenerationEditorController>("Gallery Generation");
    }

    private void OnGUI()
    {
        GUILayout.Label("Categories", EditorStyles.boldLabel);

        _tags = (RoomTagFlags)EditorGUILayout.EnumFlagsField("Select Tags", _tags);

        //if (GUILayout.Button("GENERATE"))
        //    TagSelector.ForceNewRequest(_tags);
    }
}
