using System.Text;
using TMPro;
using UnityEngine;

namespace KronosTech.UI.ChangeLog
{
    public class ConvertChangeLogToTMP : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int m_sizeH1 = 12;
        [SerializeField] private int m_sizeH2 = 10;
        [SerializeField] private int m_sizeH3 = 8;
        [SerializeField] private int m_sizeNormal = 6;
        [SerializeField, TextArea(10, 40)] private string m_changeLogMarkdown;
        [Header("References")]
        [SerializeField] private TextMeshProUGUI m_outputText;

        private void Start()
        {
            if(m_changeLogMarkdown != null)
            {
                m_outputText.text = ConvertMarkdownToTMP(m_changeLogMarkdown);
            }
        }

        public void SetChangeLog(string text)
        {
            m_changeLogMarkdown = text;

            m_outputText.text = ConvertMarkdownToTMP(m_changeLogMarkdown);
        }

        private string ConvertMarkdownToTMP(string md)
        {
            var stringBuilder = new StringBuilder();
            var lines = md.Split('\n');

            foreach (string rawLine in lines)
            {
                string line = rawLine.TrimEnd('\r');

                if (line.StartsWith("# "))
                {
                    stringBuilder.AppendLine($"<size={m_sizeH1}><b>{line.Substring(2).Trim()}</b></size>");
                    continue;
                }
                if (line.StartsWith("## "))
                {
                    stringBuilder.AppendLine($"\n<size={m_sizeH2}><b>{line.Substring(3).Trim()}</b></size>");
                    continue;
                }
                if (line.StartsWith("### "))
                {
                    stringBuilder.AppendLine($"<size={m_sizeH3}>{line.Substring(4).Trim()}</size>");
                    continue;
                }
                if (line.StartsWith("- "))
                {
                    stringBuilder.AppendLine($"<size={m_sizeNormal}>  • {line.Substring(2).Trim()}</size>");
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    stringBuilder.AppendLine("");
                    continue;
                }
                stringBuilder.AppendLine($"<size={m_sizeNormal}>{line}</size>");
            }

            return stringBuilder.ToString();
        }
    }
}