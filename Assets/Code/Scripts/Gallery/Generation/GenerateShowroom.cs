using KronosTech.AssetBundles;
using KronosTech.Data;
using KronosTech.Gallery.Generation.ObjectPooling;
using KronosTech.Gallery.Generation.Placeables;
using KronosTech.Gallery.Generation.TagSelection;
using KronosTech.Gallery.Rooms;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Generation
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
        [SerializeField] private PlaceableExit m_start;
        [SerializeField] private Transform m_roomsParent;
        [SerializeField] private ObjectsPoolGallery m_corridorsPool;
        [SerializeField] private ObjectsPoolGallery m_tilesPool;
        [SerializeField] private ObjectsPoolGallery m_wallsPool;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private RoomData[] m_loadedData;

        private readonly Dictionary<RoomData, PlaceableRoom> m_rooms = new();

        private readonly string m_dataFolderPath = "Rooms/";

        public event Action OnGenerationStart;
        public event Action<bool> OnGenerationEnd;

        private bool m_isGeneratingRoom;

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
            if (m_isGeneratingRoom)
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
            PlaceableExit nextExit = null;
            var roomIndex = 0;

            m_isGeneratingRoom = true;

            while (remainingRooms > 0)
            {
                var corridor = (PlaceableCorridor)m_corridorsPool.GetRandomObject();
                corridor.Place(nextExit != null ? nextExit : m_start);

                nextExit = corridor.GetExit;

                // Instantiate Tile
                var currentTile = (PlaceableTile)m_tilesPool.GetRandomObject();
                currentTile.Place(nextExit);
                currentTile.InitializeGallery(remainingRooms, nextExit, (PlaceableExit exit, PlaceableExit[] roomPositions) =>
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

            m_isGeneratingRoom = false;

            OnGenerationEnd?.Invoke(availableRooms.Length > 0);
        }
    }
}