using KronosTech.AssetBundles;
using KronosTech.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentDisplay
{
    public class ContentDisplayImages : ContentDisplayBase<RoomSpriteData>
    {
        [Header("References")]
        [SerializeField] private Image m_display;
        [SerializeField] private TextMeshProUGUI m_titleDisplay;

        private int m_videosToLoadCount = 0;
        private int m_loadedVideosCount = 0;

        #region ContentDisplayBase
        protected override void LoadData(Action callback)
        {
            var imagesCount = m_data.Content.Length;
            Data = new();

            for (int i = 0; i < imagesCount; i++)
            {
                var asset = m_data.Content[i].Asset;
                var index = i;

                AssetBundlesRequest.Load<Sprite>(asset.Bundle, asset.Name, (args) => SaveSpriteToDataCallback(args, index, callback));
            }
        }

        private void SaveSpriteToDataCallback(AssetBundleLoadEventArgs<Sprite> args, int index, Action callback)
        {
            m_loadedVideosCount += 1;

            if (!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(ContentDisplayImages)}.cs: " +
                    $"Failed to load image at index {index}.");

                return;
            }

            Data.Add(new RoomSpriteData(m_data.Content[index].Title, args.Asset));

            if (m_loadedVideosCount >= m_videosToLoadCount)
            {
                callback?.Invoke();
            }
        }

        protected override void ShowContent(int index)
        {
            m_display.sprite = Data[index].Sprite;
            m_titleDisplay.text = Data[index].Title;
        }
        #endregion
    }
}