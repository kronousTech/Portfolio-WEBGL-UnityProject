using KronosTech.AssetBundles;
using KronosTech.Data;
using KronosTech.ObjectPooling;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.ShowroomGeneration
{
    public class GenerateShowroom : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlaceableRoom m_galleryWall;
        [SerializeField] private PlaceableRoom m_galleryRoom;
        [Header("References")]
        [SerializeField] private AssetBundleDownloadAll m_downloader;
        [SerializeField] private TagSelector m_selector;
        [SerializeField] private Button m_generateButton;
        [SerializeField] private GalleryTileExit m_start;
        [SerializeField] private Transform m_roomsParent;
        [SerializeField] private GalleryObjectsPool m_corridorsPool;
        [SerializeField] private GalleryObjectsPool m_tilesPool;
        [SerializeField] private GalleryObjectsPool m_wallsPool;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private RoomData[] m_loadedData;

        private Dictionary<RoomData, PlaceableRoom> m_rooms = new();

        private readonly string m_dataFolderPath = "RoomsData/";

        public event Action OnGenerationStart;
        public event Action<bool> OnGenerationEnd;

        private bool m_isBuilding;

        private void OnEnable()
        {
            m_downloader.OnAllBundlesDownloaded += CacheRoomsCallback;
            m_generateButton.onClick.AddListener(() => StartCoroutine(GenerateRooms()));
        }
        private void OnDisable()
        {
            m_downloader.OnAllBundlesDownloaded -= CacheRoomsCallback;
            m_generateButton.onClick.RemoveListener(() => StartCoroutine(GenerateRooms()));
        }

        private void CacheRoomsCallback()
        {
            m_loadedData = Resources.LoadAll<RoomData>(m_dataFolderPath);

            PlaceableRoom roomPrefab;
            PlaceableRoom roomInstantiated;

            foreach (var data in m_loadedData)
            {
                roomPrefab = data.HasVideos() ? m_galleryRoom : m_galleryWall;
                roomInstantiated = GameObject.Instantiate(roomPrefab, m_roomsParent);
                roomInstantiated.Initialize(data);
                roomInstantiated.SetVisibility(false);

                m_rooms.Add(data, roomInstantiated);
            }
        }
        
        public RoomData[] GetSelectedRooms()
        {
            var currentTags = m_selector.GetTags();
            var selectedRooms = new List<RoomData>();

            for (int i = 0; i < m_loadedData.Length; i++)
            {
                if (m_loadedData[i].Tags.HasAny(currentTags))
                {
                    selectedRooms.Add(m_loadedData[i]);
                }
            }

            return selectedRooms.ToArray();
        }

        private void HideAllRooms()
        {
            foreach (var item in m_rooms)
            {
                item.Value.SetVisibility(false);
            }
        }

        private IEnumerator GenerateRooms() 
        {
            if (m_isBuilding)
            {
                yield break;
            }

            m_tilesPool.ClearObjects();
            m_corridorsPool.ClearObjects();
            m_wallsPool.ClearObjects();
            
            HideAllRooms();

            yield return null;

            OnGenerationStart?.Invoke();

            var availableRooms = GetSelectedRooms();
            var remainingRooms = availableRooms.Length;
            GalleryTileExit nextExit = null;
            var roomIndex = 0;

            m_isBuilding = true;

            yield return null;

            while (remainingRooms > 0)
            {
                var corridor = (PlaceableCorridor)m_corridorsPool.GetRandomObject();
                corridor.Place(nextExit != null ? nextExit : m_start);

                nextExit = corridor.GetExit;

                yield return null;

                // Instantiate Tile
                var currentTile = (PlaceableTile)m_tilesPool.GetRandomObject();
                currentTile.Place(nextExit);
                currentTile.InitializeGallery(remainingRooms, nextExit, (GalleryTileExit exit, GalleryTileExit[] roomPositions) =>
                {
                    if(exit != null)
                    {
                        nextExit = exit;
                    }

                    // Add Display Rooms
                    for (int i = 0; i < roomPositions.Length; i++)
                    {
                        if(roomIndex < availableRooms.Length)
                        {
                            m_rooms[availableRooms[roomIndex]].SetVisibility(true);
                            m_rooms[availableRooms[roomIndex++]].Place(roomPositions[i]);

                            remainingRooms--;
                        }
                        else
                        {
                            var wall = ((PlaceableWall)m_wallsPool.GetRandomObject());
                            wall.Place(roomPositions[i]);
                        }
                    }
                });

                yield return null;
            }

            m_isBuilding = false;

            OnGenerationEnd?.Invoke(availableRooms.Length > 0);
        }
    }
}