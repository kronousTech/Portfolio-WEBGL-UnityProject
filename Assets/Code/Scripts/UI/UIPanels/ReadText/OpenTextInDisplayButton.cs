using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.UI.UIPanels
{
    public class OpenTextInDisplayButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text m_textDisplay;
        [SerializeField] private Button m_button;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private UIPanelReadText m_textReader;

        private void OnEnable()
        {
            m_button.onClick.AddListener(AddTextToDisplayCallback);
        }
        private void OnDisable()
        {
            m_button.onClick.RemoveListener(AddTextToDisplayCallback);
        }
        private void Awake()
        {
            m_textReader = FindFirstObjectByType<UIPanelReadText>();
        }

        private void AddTextToDisplayCallback()
        {
            m_textReader.SetText(m_textDisplay);
        }
    }
}