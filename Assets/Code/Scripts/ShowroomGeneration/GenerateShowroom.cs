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
        [SerializeField] private GalleryCorridorPool _corridorsPool;

        [Header("Parents")]
        [SerializeField] private Transform _tilesParent;

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

            _tilesParent.ClearChildren();

            _corridorsPool.ClearObjects();

            yield return null;

            OnGenerationStart?.Invoke();

            var remainingRooms = rooms.Count;
            GalleryTileExit nextExit = null;
            var roomIndex = 0;

            _startedGeneration = true;

            yield return null;

            while (remainingRooms > 0)
            {
                var corridor = (GalleryCorridor)_corridorsPool.GetRandomCorridor();
                corridor.Place(nextExit != null ? nextExit : _galleryStart);

                nextExit = corridor.GetExit;

                yield return null;

                // Instantiate Tile
                var currentTile = Instantiate(GalleryGenerationPieces.GetTile(remainingRooms), _tilesParent);
                currentTile.Initialize(remainingRooms, nextExit, (GalleryTileExit exit, GalleryTileExit[] roomPositions) =>
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
                            Instantiate(GalleryGenerationPieces.GetEndWall(), null) // ADD PARENT
                                .Place(roomPositions[i]);
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