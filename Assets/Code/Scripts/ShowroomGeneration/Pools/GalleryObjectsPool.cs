using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace KronosTech.GalleryGeneration.Pools
{
    public class GalleryObjectsPool : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _resourcesPath;
        [SerializeField] private int _preloadCount;

        private readonly Dictionary<int, IObjectPool<GalleryPoolObjectBase>> _pools = new();
        private readonly List<GalleryPoolObjectBase> _activeObjects = new();

        private void Awake()
        {
            var corridors = Resources.LoadAll<GalleryPoolObjectBase>(_resourcesPath);

            for(int i = 0; i < corridors.Length; i++)
            {
                var index = i;
                _pools.Add(index, new ObjectPool<GalleryPoolObjectBase>(
                    () => OnCreate(corridors[index], index),
                    OnGet,
                    OnRelease,
                    OnObjectDestroy,
                    false, 5));

                // Pre Load
                var cache = new List<GalleryPoolObjectBase>();

                for(int j = 0; j < _preloadCount; j++) 
                {
                    cache.Add(_pools[index].Get());
                }
                for (int j = 0; j < _preloadCount; j++)
                {
                    _pools[index].Release(cache[j]);
                }
            }
        }

        public GalleryPoolObjectBase GetRandomObject()
        {
            return _pools[Random.Range(0, _pools.Count)].Get();
        }
        public void ClearObjects()
        {
            for (int i = _activeObjects.Count-1; i >= 0; i--)
            {
                _activeObjects[i].Release();
            }
        }

        private GalleryPoolObjectBase OnCreate(GalleryPoolObjectBase obj, int index)
        { 
            var corridor = Instantiate(obj, transform);
            corridor.Initialize(_pools[index]);
            
            return corridor;
        }
        private void OnGet(GalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(true);

            _activeObjects.Add(obj);
        }
        private void OnRelease(GalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(false);

            _activeObjects.Remove(obj);
        }
        private void OnObjectDestroy(GalleryPoolObjectBase obj)
        {
            Destroy(obj.gameObject);
        }
    }
}

