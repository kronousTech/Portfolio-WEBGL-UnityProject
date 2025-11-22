using KronosTech.CustomPackage.Utilities.Extensions;
using KronosTech.AssetBundles;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Customization.GalleryPictures
{
    public class LoaderGalleryPictures : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string m_bundleName;
        [SerializeField] private AssetBundleDownloadAll m_downloader;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private List<Sprite> m_picturesList = new();
        [SerializeField, ReadOnly] private int m_index = 0;

        private void OnEnable()
        {
            m_downloader.OnAllBundlesDownloaded += LoadPicturesAssetBundleCallback;
        }
        private void OnDisable()
        {
            m_downloader.OnAllBundlesDownloaded -= LoadPicturesAssetBundleCallback;
        }

        private void LoadPicturesAssetBundleCallback()
        {
            var bundle = AssetBundlesRequest.GetBundle(m_bundleName);

            foreach (var name in bundle.GetAllAssetNames())
            {
                m_picturesList.Add(bundle.LoadAsset<Sprite>(name));
            }

            m_picturesList.Shuffle();
        }

        public Sprite RequestPicture()
        {
            if(m_picturesList.Count == 0)
            {
                return null;
            }

            if(m_index >= m_picturesList.Count)
            {
                m_index = 0;

                m_picturesList.Shuffle();
            }

            return m_picturesList[m_index++];
        }
    }
}