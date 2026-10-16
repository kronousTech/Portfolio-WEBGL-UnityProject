using UnityEngine;
using UnityEngine.Pool;

namespace KronosTech.Gallery.Generation.ObjectPooling
{
    public class PoolObjectGalleryBase : MonoBehaviour
    {
        private IObjectPool<PoolObjectGalleryBase> m_pool;

        public void Initialize(IObjectPool<PoolObjectGalleryBase> pool)
        {
            m_pool = pool;
        }
        public virtual void Release()
        {
            m_pool.Release(this);
        }
    }
}
