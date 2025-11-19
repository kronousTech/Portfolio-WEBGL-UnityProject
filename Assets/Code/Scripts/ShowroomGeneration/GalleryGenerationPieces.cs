using KronosTech.ShowroomGeneration;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public static class GalleryGenerationPieces
{
    private static PlaceableTile _lastTile;
    private static List<PlaceableTile> _tilesPrefabs;
    private static PlaceableCorridor[] _corridorPrefabs;
    
    private static PlaceableRoom _endWall;

    private static Transform _corridorObjectsParent;
    private static readonly Dictionary<PlaceableCorridor, ObjectPool<PlaceableCorridor>> _corridorPools = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() 
    {
        _tilesPrefabs = Resources.LoadAll<PlaceableTile>("GalleryGeneration/Tiles").ToList();
        _endWall = Resources.Load<PlaceableRoom>("GalleryGeneration/Wall/Wall");
    }

    public static PlaceableTile GetTile(int remainingRoomsCount)
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
    
    public static PlaceableRoom GetEndWall() => _endWall;

   
}