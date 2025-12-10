using KronosTech.CustomPackage.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Map
{
    public class MapElementDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_sprite;
        [SerializeField] private Material m_priorityMaterial;

        public void Setup(MapElementSourceData data)
        {
            if(data.Priority)
            {
                m_sprite.material = m_priorityMaterial;
            }

            name = data.Transform.name;
            m_sprite.sprite = data.Sprite;
            transform.GetRectTransform().sizeDelta = data.Sprite.rect.size;

            Place(data.Transform);
        }

        public void Place(Transform worldTransform)
        {
            transform.localPosition = new Vector3(worldTransform.position.x, worldTransform.position.z, 0);
            transform.localEulerAngles = new Vector3(0, 0, -worldTransform.eulerAngles.y);
        }
    }
}