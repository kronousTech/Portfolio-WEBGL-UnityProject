using UnityEngine;
using UnityEngine.Pool;

public abstract class IGalleryPoolObjectBase : MonoBehaviour
{
    private IObjectPool<IGalleryPoolObjectBase> _pool;

    public void Initialize(IObjectPool<IGalleryPoolObjectBase> pool) 
    {
        _pool = pool;
    }
    public void Release()
    {
        _pool.Release(this);
    }
}
