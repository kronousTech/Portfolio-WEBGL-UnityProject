using UnityEngine;
using UnityEngine.Events;

namespace KronosTech.Gallery.Rooms.Optimization
{
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
}