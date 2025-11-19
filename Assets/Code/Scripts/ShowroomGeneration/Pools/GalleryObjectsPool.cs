using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace KronosTech.ObjectPooling
{
    public class GalleryObjectsPool : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _resourcesPath;
        [SerializeField] private int _preloadCount;

        private readonly Dictionary<int, IObjectPool<GalleryPoolObjectBase>> m_pools = new();
        private readonly List<GalleryPoolObjectBase> m_activeObjects = new();

        private void Awake()
        {
            var objects = Resources.LoadAll<GalleryPoolObjectBase>(_resourcesPath);

            for(int i = 0; i < objects.Length; i++)
            {
                var index = i;
                m_pools.Add(index, new ObjectPool<GalleryPoolObjectBase>(
                    () => OnCreate(objects[index], index),
                    OnGet,
                    OnRelease,
                    OnObjectDestroy,
                    false, 5));

                // Pre Load
                var cache = new List<GalleryPoolObjectBase>();

                for(int j = 0; j < _preloadCount; j++) 
                {
                    cache.Add(m_pools[index].Get());
                }
                for (int j = 0; j < _preloadCount; j++)
                {
                    m_pools[index].Release(cache[j]);
                }
            }
        }

        public GalleryPoolObjectBase GetRandomObject()
        {
            return m_pools[Random.Range(0, m_pools.Count)].Get();
        }
        public void ClearObjects()
        {
            for (int i = m_activeObjects.Count-1; i >= 0; i--)
            {
                m_activeObjects[i].Release();
            }
        }

        private GalleryPoolObjectBase OnCreate(GalleryPoolObjectBase obj, int index)
        { 
            var corridor = Instantiate(obj, transform);
            corridor.Initialize(m_pools[index]);
            
            return corridor;
        }
        private void OnGet(GalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(true);

            m_activeObjects.Add(obj);
        }
        private void OnRelease(GalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(false);

            m_activeObjects.Remove(obj);
        }
        private void OnObjectDestroy(GalleryPoolObjectBase obj)
        {
            Destroy(obj.gameObject);
        }
    }
}

