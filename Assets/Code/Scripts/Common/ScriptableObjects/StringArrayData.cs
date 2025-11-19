using UnityEngine;

[CreateAssetMenu(fileName = "StringArrayData", menuName = "Scriptable Objects/StringArrayData")]
public class StringArrayData : ScriptableObject
{
    [SerializeField] private string[] m_strings;

    public string[] Strings
    {
        get { return m_strings; }
    }
}
