using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

namespace KronosTech.GalleryGeneration.Pools
{
    public class GalleryCorridorPool : MonoBehaviour
    {
        private Transform _parent;

        private readonly Dictionary<int, IObjectPool<IGalleryPoolObjectBase>> _pools = new();

        private List<IGalleryPoolObjectBase> _activeObjects = new();

        private const int PRE_LOAD_COUNT = 5;

        private void Awake()
        {
            _parent = new GameObject("Corridors pool").transform;
            _parent.parent = transform;

            var corridors = Resources.LoadAll<IGalleryPoolObjectBase>("GalleryGeneration/Corridors");

            for(int i = 0; i < corridors.Length; i++)
            {
                var index = i;
                _pools.Add(index, new ObjectPool<IGalleryPoolObjectBase>(
                    () => OnCreate(corridors[index], index),
                    OnGet,
                    OnRelease,
                    OnObjectDestroy,
                    false, 5));

                // Pre Load
                var cache = new List<IGalleryPoolObjectBase>();

                for(int j = 0; j < PRE_LOAD_COUNT; j++) 
                {
                    cache.Add(_pools[index].Get());
                }
                for (int j = 0; j < PRE_LOAD_COUNT; j++)
                {
                    _pools[index].Release(cache[j]);
                }
            }
        }

        public IGalleryPoolObjectBase GetRandomCorridor()
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


        private IGalleryPoolObjectBase OnCreate(IGalleryPoolObjectBase obj, int index)
        {
            var corridor = Instantiate(obj, _parent);
            corridor.Initialize(_pools[index]);
            
            return corridor;
        }
        private void OnGet(IGalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(true);

            _activeObjects.Add(obj);
        }
        private void OnRelease(IGalleryPoolObjectBase obj)
        {
            obj.gameObject.SetActive(false);

            _activeObjects.Remove(obj);
        }
        private void OnObjectDestroy(IGalleryPoolObjectBase obj)
        {
            Destroy(obj.gameObject);
        }
    }
}

