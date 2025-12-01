using UnityEngine;
using UnityEngine.Pool;

namespace KronosTech.Gallery.Generation.ObjectPooling
{
    public class PoolObjectGalleryBase : MonoBehaviour
    {
        private IObjectPool<PoolObjectGalleryBase> _pool;

        public void Initialize(IObjectPool<PoolObjectGalleryBase> pool)
        {
            _pool = pool;
        }
        public void Release()
        {
            _pool.Release(this);
        }
    }
}
