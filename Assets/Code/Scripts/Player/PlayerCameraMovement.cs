using KronosTech.InputSystem;
using UnityEngine;

namespace KronosTech.Player
{
    public class PlayerCameraMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform m_cameraTransform;
        [SerializeField] private float m_rotationSpeed = 2.0f;
        [SerializeField] private float m_clampTop = 90.0f;
        [SerializeField] private float m_clampBot = -90.0f;
        [Header("References")]
        [SerializeField] private StarterAssetsInputs m_input;

        private float m_targetPitch;
        private float _rotationVelocity;

        private readonly float _interpolateValue = 5.5f;

        private void LateUpdate()
        {
            // Up Down
            m_targetPitch += m_input.look.y * m_rotationSpeed;
            m_targetPitch = Mathf.Clamp(m_targetPitch, m_clampBot, m_clampTop);
            m_cameraTransform.localEulerAngles = new Vector3(m_targetPitch, 0.0f, 0.0f);

            // Left right
            _rotationVelocity = m_input.look.x * m_rotationSpeed;
            transform.Rotate(Vector3.up * _rotationVelocity);
        }

        public float GetRotationSpeed()
        {
            return m_rotationSpeed;
        }
        public void SetRotationSpeed(float speed)
        {
            m_rotationSpeed = speed;
        }
    }
}