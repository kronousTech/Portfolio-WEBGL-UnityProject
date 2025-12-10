using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapRenderer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MapElementDisplay m_prefab;
        [SerializeField] private RectTransform m_parent;

        private readonly Dictionary<int, MapElementDisplay> m_elements = new();

        private void OnEnable()
        {
            MapMediator.OnAdded += AddElementCallback;
            MapMediator.OnUpdate += UpdateElementCallback;
            MapMediator.OnRemoved += RemoveElementCallback;
        }
        private void OnDisable()
        {
            MapMediator.OnAdded -= AddElementCallback;
            MapMediator.OnUpdate -= UpdateElementCallback;
            MapMediator.OnRemoved -= RemoveElementCallback;
        }

        private void AddElementCallback(MapElementSourceData data)
        {
            var newElement = Instantiate(m_prefab, m_parent);
            newElement.Setup(data.Sprite, data.Transform);

            m_elements.Add(data.ID, newElement);
        }
        private void UpdateElementCallback(MapElementSourceData data)
        {
            m_elements[data.ID].Place(data.Transform);
        }
        private void RemoveElementCallback(MapElementSourceData data)
        {
            Destroy(m_elements[data.ID].gameObject);

            m_elements.Remove(data.ID);
        }
    }
}