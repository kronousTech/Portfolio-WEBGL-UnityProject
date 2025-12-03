using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.UI.UIPanels
{ 
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        [Header("References Base")]
        [SerializeField] private Button m_closeButton;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private bool m_isOpen;
        [SerializeField, ReadOnly] private Animator m_animator;
        [SerializeField, ReadOnly] private UIPanel[] m_otherDisplays;

        private readonly string m_openParameter = "Open";

        public static event Action<bool> OnToggle;

        public bool IsOpen
        {
            get { return m_isOpen; }
        }

        protected virtual void OnEnable()
        {
            m_closeButton.onClick.AddListener(ClosePanelCallback);
        }
        protected virtual void OnDisable()
        {
            m_closeButton.onClick.RemoveListener(ClosePanelCallback);
        }
        protected virtual void Awake()
        {
            if (!TryGetComponent(out m_animator))
            {
                Debug.LogError($"{nameof(UIPanel)}.cs: " +
                    $"Failed to find Animator component on GameObject");

                return;
            }

            m_otherDisplays = FindObjectsByType<UIPanel>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }

        private void ClosePanelCallback()
        {
            ClosePanel();
        }

        public void TogglePanel()
        {
            if (AnotherDisplayIsOpened())
            {
                return;
            }

            m_isOpen = !m_isOpen;

            m_animator.SetBool(m_openParameter, m_isOpen);

            OnToggle?.Invoke(m_isOpen);
        }

        public void ClosePanel()
        {
            if (m_isOpen)
            {
                TogglePanel();
            }
        }

        private bool AnotherDisplayIsOpened()
        {
            foreach (var display in m_otherDisplays)
            {
                if (display == this)
                {
                    continue;
                }
                else if (display.IsOpen)
                {
                    return true;
                }
            }

            return false;
        }
    }
}