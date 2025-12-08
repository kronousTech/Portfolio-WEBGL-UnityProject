using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementsBuilder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MapElementDisplay m_prefab;
        [SerializeField] private RectTransform m_parent;

        private Dictionary<int, MapElementDisplay> m_elements = new();

        public void DisplayElement(int m_ID, Sprite m_mapImage, Transform transform)
        {
            if (m_elements.ContainsKey(m_ID))
            {
                m_elements[m_ID].Place(transform);
            }
            else
            {
                var newElement = Instantiate(m_prefab, m_parent);
                newElement.Setup(m_mapImage, transform);
                
                m_elements.Add(m_ID, newElement); 
            }
        }
    }
}