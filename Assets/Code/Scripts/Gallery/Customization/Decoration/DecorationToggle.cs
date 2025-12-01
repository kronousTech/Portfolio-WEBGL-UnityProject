using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Customization.Decoration
{
    public class DecorationToggle : MonoBehaviour
    {
        private Toggle m_toggle;

        private void OnEnable()
        {
            m_toggle.onValueChanged.AddListener(DecorationController.SetVisibility);
        }
        private void OnDisable()
        {
            m_toggle.onValueChanged.AddListener(DecorationController.SetVisibility);
        }
        private void Awake()
        {
            m_toggle = GetComponent<Toggle>();
        }
    }
}