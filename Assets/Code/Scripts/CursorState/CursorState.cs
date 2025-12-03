using KronosTech.UI.UIPanels;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool m_lockedOnStart;

    private CursorLockMode m_lockMode;

    private void OnEnable()
    {
        UIPanel.OnToggle += (open) => SetCursorLockState(!open);
    }
    private void OnDisable()
    {
        UIPanel.OnToggle -= (open) => SetCursorLockState(!open);
    }
    private void Awake()
    {
        SetCursorLockState(m_lockedOnStart);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = m_lockMode;
    }

    public void SetCursorLockState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;

        m_lockMode = Cursor.lockState;
    }
}