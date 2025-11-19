using System.IO;
using UnityEditor;
using UnityEngine;

namespace KronosTech.CustomPackage.Utilities
{
    public static class EditorToolOpenPersistentDataPath
    {
        [MenuItem("KronosTech/Open Persistent Data Path")]
        public static void OpenPersistentDataPath()
        {
            string path = Application.persistentDataPath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            EditorUtility.RevealInFinder(path);
        }
    }
}