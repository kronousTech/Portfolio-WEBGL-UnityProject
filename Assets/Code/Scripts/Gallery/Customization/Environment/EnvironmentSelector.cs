using KronosTech.CustomPackage.Utilities.Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Customization.Environment
{
    public class EnvironmentSelector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EnvironmentDataLoader m_loader;
        [SerializeField] private EnvironmentToggleDisplay m_togglePrefab;
        [SerializeField] private RectTransform m_parent;
        [SerializeField] private ToggleGroup m_toggleGroup;

        public event Action<EnvironmentData> OnEnvironmentSelected;

        private void OnEnable()
        {
            m_loader.OnEnvironmentsLoaded += BuildEnvironmentTogglesCallback;
        }
        private void OnDisable()
        {
            m_loader.OnEnvironmentsLoaded -= BuildEnvironmentTogglesCallback;
        }

        private void BuildEnvironmentTogglesCallback(EnvironmentData[] data)
        {
            m_parent.DestroyChildren();

            foreach (var environment in data)
            {
                var display = Instantiate(m_togglePrefab, m_parent);
                display.Setup(environment, m_toggleGroup);

                display.OnSelected += RaiseEnvironmentSelectedCallback;
            }

            OnEnvironmentSelected?.Invoke(data[0]);
        }

        private void RaiseEnvironmentSelectedCallback(EnvironmentData data)
        {
            OnEnvironmentSelected?.Invoke(data);
        }
    }
}