using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderViewDistance : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int m_startValue = 75;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_viewDistanceText;
    [SerializeField] private Slider m_slider;
    [SerializeField] private Camera m_camera;

    private void OnEnable()
    {
        m_slider.onValueChanged.AddListener(UpdateTextCallback);
        m_slider.onValueChanged.AddListener(UpdateCameraFarClipPlaneCallback);
    }
    private void OnDisable()
    {
        m_slider.onValueChanged.RemoveListener(UpdateTextCallback);
        m_slider.onValueChanged.RemoveListener(UpdateCameraFarClipPlaneCallback);
    }
    private void Start()
    {
        UpdateTextCallback(m_startValue);
        UpdateCameraFarClipPlaneCallback(m_startValue);
    }

    private void UpdateTextCallback(float value)
    {
        m_viewDistanceText.text = ((int)value).ToString();
    }
    private void UpdateCameraFarClipPlaneCallback(float value)
    {
        m_camera.farClipPlane = value;
    }
}