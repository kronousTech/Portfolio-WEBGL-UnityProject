using UnityEngine;

namespace KronosTech.Gallery.Customization
{
    public class CustomizableRenderer : MonoBehaviour
    {
        private MeshRenderer m_renderer;

        private void OnEnable()
        {
            GalleryCustomization.AddCustomizableRenderer(this);
        }
        private void OnDisable()
        {
            GalleryCustomization.RemoveCustomizableRenderer(this);
        }
        private void Awake()
        {
            m_renderer = GetComponent<MeshRenderer>();
        }

        public void ReplaceMaterial(string materialName, Material material)
        {
            var shared = m_renderer.sharedMaterials;

            for (int i = 0; i < shared.Length; i++)
            {
                if (shared[i] != null && shared[i].name.Contains(materialName))
                {
                    shared[i] = material;

                    m_renderer.sharedMaterials = shared;

                    return;
                }
            }
        }
    }
}