using NaughtyAttributes;
using System;
using UnityEngine;

namespace KronosTech.Gallery.Customization.Environment
{
    public class EnvironmentDataLoader : MonoBehaviour
    {
        [Header("Debug View")]
        [SerializeField, ReadOnly] private EnvironmentData[] m_environments;

        private readonly string m_dataPath = "Gallery/Customization/Environments";

        public event Action<EnvironmentData[]> OnEnvironmentsLoaded;

        private void Start()
        {
            m_environments = Resources.LoadAll<EnvironmentData>(m_dataPath);

            OnEnvironmentsLoaded?.Invoke(m_environments);
        }
    }
}