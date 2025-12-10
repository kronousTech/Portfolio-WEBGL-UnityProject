using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderViewDistance : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int m_startValue = 50;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_viewDistanceText;
    private Camera m_camera;
    private Slider m_slider;

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
    private void Awake()
    {
        m_camera = Camera.main;
        if (m_camera == null)
        {
            Debug.LogError("Didn't found Camera on FieldOfViewSlider");
            return;
        }
        m_slider = GetComponent<Slider>();
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