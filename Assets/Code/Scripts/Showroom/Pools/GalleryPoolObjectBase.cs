using UnityEngine;
using UnityEngine.Pool;

namespace KronosTech.ObjectPooling
{
    public class GalleryPoolObjectBase : MonoBehaviour
    {
        private IObjectPool<GalleryPoolObjectBase> _pool;

        public void Initialize(IObjectPool<GalleryPoolObjectBase> pool)
        {
            _pool = pool;
        }
        public void Release()
        {
            _pool.Release(this);
        }
    }
}
