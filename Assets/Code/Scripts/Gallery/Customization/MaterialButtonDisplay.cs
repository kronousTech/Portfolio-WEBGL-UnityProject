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
            if (material.HasTexture("_MainTex") && material.mainTexture != null)
            {
                var texture2D = TextureToTexture2D(material.mainTexture);

                m_image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            }

            if (material.HasProperty("_Color"))
            {
                m_image.color = material.color;
            }
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
    }
}
