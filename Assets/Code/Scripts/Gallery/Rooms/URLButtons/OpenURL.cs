using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Rooms.URLButtons
{
    [RequireComponent(typeof(Button))]
    public class OpenURL : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string m_url;

        private Button m_button;

        private void OnEnable()
        {
            m_button.onClick.AddListener(OpenWebLinkCallback);
        }
        private void OnDisable()
        {
            m_button.onClick.RemoveListener(OpenWebLinkCallback);
        }
        private void Awake()
        {
            m_button = GetComponent<Button>();
        }

        private void OpenWebLinkCallback()
        {
            if (!Application.isFocused)
            {
                return;
            }

            Application.OpenURL(m_url);
        }

        public void SetNewURL(string url) => m_url = url;
    }
}
