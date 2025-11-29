using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Customization
{
    public class MaterialButtonDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_image;
        [SerializeField] private Toggle m_toggle;

        public Toggle Toggle
        {
            get { return m_toggle; }
        }

        private void Awake()
        {
            m_image = GetComponent<Image>();
        }

        public void Initialize(Material material)
        {
            // Skybox
            if (material.HasProperty("_Tex"))
            {
                var texture2D = ConvertCubemapToTexture2D((Cubemap)material.GetTexture("_Tex"));

                m_image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            }
            else if (material.HasTexture("_MainTex") && material.mainTexture != null)
            {
                var texture2D = TextureToTexture2D(material.mainTexture);

                m_image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            }
            
            if(material.HasProperty("_Color"))
                m_image.color = material.color;
        }

        private static Texture2D TextureToTexture2D(Texture texture)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
            Graphics.Blit(texture, renderTexture);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;

            Texture2D texture2D = new Texture2D(texture.width, texture.height);
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);

            return texture2D;
        }

        Texture2D ConvertCubemapToTexture2D(Cubemap cubemap)
        {
            var width = cubemap.width;
            var height = cubemap.height;

            // Create a new Texture2D
            var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Set pixels from the specified face of the cubemap
            var colors = cubemap.GetPixels(CubemapFace.PositiveY);
            texture.SetPixels(colors);

            // Apply changes
            texture.Apply();

            return texture;
        }
    }
}
