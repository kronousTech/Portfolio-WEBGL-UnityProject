using KronosTech.AssetManagement;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
    public class RoomData : ScriptableObject
    {
        public string ProjectName;
        public string ClientName;
        public RoomType RoomType;
        public RoomTagFlags Tags;
        public List<InfoCategory> Texts;
        public ContentData[] Images;
        public ContentData[] Videos;
        public RoomClickableLinkData[] ClickableLinks;

        public string GetFullName()
        {
            return $"{ClientName}-{ProjectName}";
        }
        public bool HasVideos()
        {
            return Videos.Length > 0;
        }
    }
}