using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SliderViewDistance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fieldOfViewText;
    private CinemachineCamera _camera;
    private Slider _slider;

    private void Awake()
    {
        _camera = FindFirstObjectByType<CinemachineCamera>();
        if (_camera == null)
        {
            Debug.LogError("Didn't found Camera on FieldOfViewSlider");
            return;
        }
        _slider = GetComponent<Slider>();   
    }
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(UpdateText);
        _slider.onValueChanged.AddListener((value) => _camera.Lens.FarClipPlane = value);
    }
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateText);
        _slider.onValueChanged.RemoveListener((value) => _camera.Lens.FarClipPlane = value);
    }

    private void UpdateText(float value)
    {
        _fieldOfViewText.text = ((int)value).ToString();
    }
}