using UnityEngine;
using UnityEngine.InputSystem;

namespace KronosTech.UI.UIPanels
{
    public class UIPanelOpenWithInput : UIPanel
    {
        [Header("Settings")]
        [SerializeField] private InputActionReference m_actionRef;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_actionRef.action.Enable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            m_actionRef.action.Disable();
        }
        protected override void Awake()
        {
            base.Awake();

            m_actionRef.action.performed += (a) => TogglePanel();
        }
    }
}