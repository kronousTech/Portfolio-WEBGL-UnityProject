using KronosTech.CustomPackage.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Map
{
    public class MapElementDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_sprite;

        public void Setup(Sprite m_mapImage, Transform worldTransform)
        {
            m_sprite.sprite = m_mapImage;
            transform.GetRectTransform().sizeDelta = m_mapImage.rect.size;

            Place(worldTransform);
        }

        public void Place(Transform worldTransform)
        {
            transform.localPosition = new Vector3(worldTransform.position.x, worldTransform.position.z, 0);
            transform.localEulerAngles = new Vector3(0, 0, -worldTransform.eulerAngles.y);
        }
    }
}