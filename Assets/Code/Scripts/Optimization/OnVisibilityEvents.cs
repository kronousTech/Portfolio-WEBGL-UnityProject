using UnityEngine;
using UnityEngine.Events;

public class OnVisibilityEvents : MonoBehaviour
{
    public UnityEvent<bool> OnVisible;
    public UnityEvent OnBecameVisibleEvent;

    private void OnBecameVisible()
    {
        OnVisible?.Invoke(true);

        OnBecameVisibleEvent?.Invoke();
    }
    private void OnBecameInvisible()
    {
        OnVisible?.Invoke(false);
    }
}
