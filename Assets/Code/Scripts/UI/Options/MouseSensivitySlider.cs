using KronosTech.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class MouseSensivitySlider : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sensivityText;
        
        private PlayerCameraMovement m_cameraMovement;

        private void Awake()
        {
            m_cameraMovement = FindFirstObjectByType<PlayerCameraMovement>();
            if (m_cameraMovement == null)
            {
                Debug.LogError("Didn't found FirstPersonController on MouseSensivitySlider");
                return;
            }

            GetComponent<Slider>().onValueChanged.AddListener(SetMouseSensivity);
            GetComponent<Slider>().value = m_cameraMovement.GetRotationSpeed() / 10f;
        }

        private void SetMouseSensivity(float value)
        {
            var newMouseSensivity = value * 10f;

            m_cameraMovement.SetRotationSpeed(newMouseSensivity);

            _sensivityText.text = m_cameraMovement.GetRotationSpeed().ToString("0.0#");
        }
    }
}