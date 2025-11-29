using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace KronosTech.Gallery.Customization
{
    public class CustomizableDecal : MonoBehaviour
    {
        private DecalProjector m_decal;

        private void OnEnable()
        {
            GalleryCustomization.AddCustomizableDecal(this);
        }
        private void OnDisable()
        {
            GalleryCustomization.RemoveCustomizableDecal(this);
        }
        private void Awake()
        {
            m_decal = GetComponent<DecalProjector>();
        }

        public void ReplaceMaterial(Material material)
        {
            m_decal.material = material;
        }
    }
}