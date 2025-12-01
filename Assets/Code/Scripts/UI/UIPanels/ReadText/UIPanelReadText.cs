using KronosTech.CustomPackage.Utilities;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace KronosTech.UI.UIPanels
{
    public class UIPanelReadText : UIPanel
    {
        [Header("Setings")]
        [SerializeField] private int m_defaultFontSize = 22;
        [Header("References")]
        [SerializeField] private TextMeshProUGUI m_textDisplay;
        [SerializeField] private ToggleExtended[] m_zoomToggles;

        protected override void OnEnable()
        {
            base.OnEnable();

            float multiplier = 1.0f;

            for (int i = 0; i < m_zoomToggles.Length; i++)
            {
                var currentMultiplier = multiplier;
                m_zoomToggles[i].OnTurnedOn.AddListener(
                    () => SetFontSizeCallback(currentMultiplier));

                multiplier += 0.1f;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            float multiplier = 1.0f;

            for (int i = 0; i < m_zoomToggles.Length; i++)
            {
                var currentMultiplier = multiplier;
                m_zoomToggles[i].OnTurnedOn.RemoveListener(
                    () => SetFontSizeCallback(currentMultiplier));

                multiplier += 0.1f;
            }
        }

        public void SetText(TMP_Text textComponent)
        {
            var text  = textComponent.text;
            var noOpenTags = Regex.Replace(text, "<size=\\d+>", "");
            var noCloseTags = noOpenTags.Replace("</size>", "");

            m_textDisplay.text = noCloseTags;

            TogglePanel();
        }
        private void SetFontSizeCallback(float multiplier)
        {
            m_textDisplay.fontSize = m_defaultFontSize * multiplier;
        }
    }
}