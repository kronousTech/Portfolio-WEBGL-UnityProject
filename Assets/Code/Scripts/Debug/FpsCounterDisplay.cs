using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class FpsCounterDisplay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Gradient m_performanceGradient;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_text;
    [Header("Debug View")]
    [SerializeField, ReadOnly] private int m_framesCount = 0;
    [SerializeField, ReadOnly] private float m_timeLeft = 0;

    private const float k_interval = 1f;
    private const float k_maxFramesReference = 60f;

    private void Update()
    {
        m_timeLeft -= Time.deltaTime;
        m_framesCount++;

        if (m_timeLeft <= 0.0)
        {
            var count = m_framesCount / k_maxFramesReference;

            m_text.color = m_performanceGradient.Evaluate(count);
            m_text.text = m_framesCount.ToString();

            m_timeLeft = k_interval;
            m_framesCount = 0;
        }
    }
}