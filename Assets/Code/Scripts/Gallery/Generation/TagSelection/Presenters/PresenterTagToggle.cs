using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Generation.TagSelection
{
    public class PresenterTagToggle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Color m_toggledColor = Color.white;
        [SerializeField] private Color m_notToggledColor = Color.white;
        [Header("References")]
        [SerializeField] private TextMeshProUGUI m_tagText;
        [SerializeField] private TagToggle m_tagToggle;
        [SerializeField] private Toggle m_toggle;

        private void OnEnable()
        {
            m_tagToggle.OnInitialized += SetTextCallback;

            m_toggle.onValueChanged.AddListener(SetTextColorCallback);
        }
        private void OnDisable()
        {
            m_tagToggle.OnInitialized -= SetTextCallback;

            m_toggle.onValueChanged.RemoveListener(SetTextColorCallback);
        }
        private void Awake()
        {
            m_toggle = GetComponent<Toggle>();

            SetTextColorCallback(m_toggle.isOn);
        }

        private void SetTextCallback(string text)
        {
            m_tagText.text = text;
        }
        private void SetTextColorCallback(bool toggled)
        {
            m_tagText.color = toggled ? m_toggledColor : m_notToggledColor;
        }
    }
}