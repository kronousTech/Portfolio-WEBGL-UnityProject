using KronosTech.AssetBundles;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentViewer
{
    public class ContentViewerImages : ContentViewerBase<ContentDataAssetData>
    {
        [Header("References")]
        [SerializeField] private Image m_display;

        #region ContentDisplayBase
        private void SaveSpriteToDataCallback(AssetBundleLoadEventArgs<Sprite> args, ContentDataAssetData content)
        {
            if (!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(ContentViewerImages)}.cs: " +
                    $"Failed to load image.");

                return;
            }

            m_display.sprite = args.Asset;
            //m_titleDisplay.text = content.Title;
        }

        protected override void ShowContent(ContentDataAssetData content)
        {
            AssetBundlesRequest.Load<Sprite>(content.Data.Bundle, content.Data.Name, (args) => SaveSpriteToDataCallback(args, content));
        }
        #endregion
    }
}