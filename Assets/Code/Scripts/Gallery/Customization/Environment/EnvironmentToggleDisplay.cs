using KronosTech.CustomPackage.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Customization.Environment
{
    public class EnvironmentToggleDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_displayImage;
        [SerializeField] private ToggleExtended m_toggle;

        private EnvironmentData m_dataCache;

        public event Action<EnvironmentData> OnSelected;

        private void OnEnable()
        {
            m_toggle.OnTurnedOn.AddListener(RaiseOnSelectedCallback);
        }
        private void OnDisable()
        {
            m_toggle.OnTurnedOn.RemoveListener(RaiseOnSelectedCallback);
        }

        public void Setup(EnvironmentData data, ToggleGroup toggleGroup)
        {
            m_dataCache = data;
            m_toggle.group = toggleGroup;
            m_displayImage.sprite = data.DisplayIcon;
        }

        private void RaiseOnSelectedCallback()
        {
            OnSelected?.Invoke(m_dataCache);
        }
    }
}