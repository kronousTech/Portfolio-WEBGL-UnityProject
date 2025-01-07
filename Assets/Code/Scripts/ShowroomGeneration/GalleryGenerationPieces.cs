using KronosTech.ShowroomGeneration;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public static class GalleryGenerationPieces
{
    private static GalleryTile _lastTile;
    private static List<GalleryTile> _tilesPrefabs;
    private static GalleryCorridor[] _corridorPrefabs;
    
    private static GalleryRoom _endWall;

    private static Transform _corridorObjectsParent;
    private static readonly Dictionary<GalleryCorridor, ObjectPool<GalleryCorridor>> _corridorPools = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() 
    {
        _tilesPrefabs = Resources.LoadAll<GalleryTile>("GalleryGeneration/Tiles").ToList();
        _endWall = Resources.Load<GalleryRoom>("GalleryGeneration/Wall/Wall");
    }

    public static GalleryTile GetTile(int remainingRoomsCount)
    {
        if(_lastTile == null)
        {
            _lastTile = _tilesPrefabs[Random.Range(0, _tilesPrefabs.Count)];

            return _lastTile;
        }
        else
        {
            var indexOfLastTile = _tilesPrefabs.IndexOf(_lastTile);
            var tempTile = _tilesPrefabs[^1];

            _tilesPrefabs[indexOfLastTile] = tempTile;
            _tilesPrefabs[^1] = _lastTile;

            _lastTile = _tilesPrefabs[Random.Range(0, _tilesPrefabs.Count - 1)];

            return _lastTile;
        }
    } 
    
    public static GalleryRoom GetEndWall() => _endWall;

   
}