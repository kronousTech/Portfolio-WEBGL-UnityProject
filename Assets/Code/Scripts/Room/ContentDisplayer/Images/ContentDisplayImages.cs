using KronosTech.AssetBundles;
using KronosTech.Data;
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

        #region ContentDisplayBase
        protected override void LoadData()
        {
            var imagesCount = m_data.Content.Length;
            Data = new RoomSpriteData[imagesCount];

            for (int i = 0; i < imagesCount; i++)
            {
                var asset = m_data.Content[i].Asset;
                var index = i;

                AssetBundlesRequest.Load<Sprite>(asset.Bundle, asset.Name, (args) => SaveSpriteToDataCallback(args, index));
            }
        }

        private void SaveSpriteToDataCallback(AssetBundleLoadEventArgs<Sprite> args, int index)
        {
            if(!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(ContentDisplayImages)}.cs: " +
                    $"Failed to load image at index {index}.");

                return;
            }

            Data[index] = new RoomSpriteData(m_data.Content[index].Title, args.Asset);
        }

        protected override void ShowContent(int index)
        {
            m_display.sprite = Data[index].Sprite;
            m_titleDisplay.text = Data[index].Title;
        }
        #endregion
    }
}