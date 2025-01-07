using KronosTech.GalleryGeneration.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GenerateShowroom : MonoBehaviour
    {
        [SerializeField] private GalleryTileExit _galleryStart;

        [Header("Requirements")]
        [SerializeField] private GalleryObjectsPool _corridorsPool;
        [SerializeField] private GalleryObjectsPool _tilesPool;
        [SerializeField] private GalleryObjectsPool _wallsPool;

        public static event Action OnGenerationStart;
        public static event Action<bool> OnGenerationEnd;

        private bool _startedGeneration;

        private void OnEnable()
        {
            RoomsSelector.OnSelection += (rooms) => StartCoroutine(GenerateRooms(rooms));
        }
        private void OnDisable()
        {
            RoomsSelector.OnSelection -= (rooms) => StartCoroutine(GenerateRooms(rooms));
        }

        private IEnumerator GenerateRooms(List<GalleryRoom> rooms) 
        {
            if (_startedGeneration)
                yield break;

            _tilesPool.ClearObjects();
            _corridorsPool.ClearObjects();
            _wallsPool.ClearObjects();

            yield return null;

            OnGenerationStart?.Invoke();

            var remainingRooms = rooms.Count;
            GalleryTileExit nextExit = null;
            var roomIndex = 0;

            _startedGeneration = true;

            yield return null;

            while (remainingRooms > 0)
            {
                var corridor = (GalleryCorridor)_corridorsPool.GetRandomObject();
                corridor.Place(nextExit != null ? nextExit : _galleryStart);

                nextExit = corridor.GetExit;

                yield return null;

                // Instantiate Tile
                var currentTile = (GalleryTile)_tilesPool.GetRandomObject();
                currentTile.InitializeGallery(remainingRooms, nextExit, (GalleryTileExit exit, GalleryTileExit[] roomPositions) =>
                {
                    if(exit != null)
                    {
                        nextExit = exit;
                    }

                    // Add Display Rooms
                    for (int i = 0; i < roomPositions.Length; i++)
                    {
                        if(roomIndex < rooms.Count)
                        {
                            rooms[roomIndex].ToggleVisibility(true);
                            rooms[roomIndex++].Place(roomPositions[i]);

                            remainingRooms--;
                        }
                        else
                        {
                            var wall = ((GalleryRoom)_wallsPool.GetRandomObject());
                            wall.Place(roomPositions[i]);
                            wall.ToggleVisibility(true);

                            //Instantiate(GalleryGenerationPieces.GetEndWall(), null) // ADD PARENT
                            //    .Place(roomPositions[i]);
                        }
                    }
                });

                yield return null;
            }

            _startedGeneration = false;

            OnGenerationEnd?.Invoke(rooms.Count > 0);
        }
    }
}