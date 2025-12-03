using UnityEngine;

namespace KronosTech.Gallery.Customization.Environment
{
    public class EnvironmentApplier : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EnvironmentSelector m_selector;
        [SerializeField] private AudioSource m_backgroundSoundSource;
        [SerializeField] private Transform m_directionalLightTransform;

        private void OnEnable()
        {
            m_selector.OnEnvironmentSelected += ApplyEnvironmentCallback;
        }
        private void OnDisable()
        {
            m_selector.OnEnvironmentSelected -= ApplyEnvironmentCallback;
        }

        private void ApplyEnvironmentCallback(EnvironmentData data)
        {
            RenderSettings.skybox = data.SkyBoxMaterial;
            RenderSettings.ambientIntensity = data.IntensityMultiplier;

            m_backgroundSoundSource.clip = data.BackgroundNoise;
            m_backgroundSoundSource.Play();

            m_directionalLightTransform.localEulerAngles = new Vector3(m_directionalLightTransform.localEulerAngles.x, data.CameraAngleY, m_directionalLightTransform.localEulerAngles.z);

            DynamicGI.UpdateEnvironment();
        }
    }
}