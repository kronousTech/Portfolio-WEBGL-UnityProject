using KronosTech.AssetBundles;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentDataArray", menuName = "Scriptable Objects/ContentDataArray")]
public class ContentDataArrayData : ScriptableObject
{
    [SerializeField] private ContentData[] m_content;

    public ContentData[] Content
    {
        get { return m_content; }
    }
}
