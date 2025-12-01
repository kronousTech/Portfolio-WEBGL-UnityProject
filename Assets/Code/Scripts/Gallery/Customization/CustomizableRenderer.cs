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
            var index = -1;

            for (int i = 0; i < m_renderer.materials.Length; i++)
            {
                if (m_renderer.materials[i].name.Contains(materialName))
                {
                    index = i;
                    break;
                }
            }

            // Found mat
            if (index > -1)
            {
                var newMats = m_renderer.sharedMaterials;
                newMats[index] = material;

                m_renderer.materials = newMats;
            }
        }
    }
}