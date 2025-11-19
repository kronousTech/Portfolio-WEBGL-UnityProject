using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.AssetBundles
{
    public class AssetBundleDownloadAll : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AssetBundleManifestLoader m_manifestLoader;

        private int m_downloadCount = 0;
        private int m_downloadAmount = 0;

        public event Action OnDownloadStart;
        public event Action<int, int> OnDownloadProgress;
        public event Action OnAllBundlesDownloaded;


        private void OnEnable()
        {
            m_manifestLoader.OnAvailableBundlesLoad += DownloadAllBundlesCallback;
        }
        private void OnDisable()
        {
            m_manifestLoader.OnAvailableBundlesLoad -= DownloadAllBundlesCallback;
        }

        private void DownloadAllBundlesCallback(List<string> availableBundles)
        {
            OnDownloadStart?.Invoke();

            m_downloadCount = 0;
            m_downloadAmount = availableBundles.Count;

            foreach (var bundle in availableBundles)
            {
                AssetBundlesRequest.Download(bundle, IncrementDownloadProgress);
            }
        }

        private void IncrementDownloadProgress(AssetBundleDownloadEventArgs args)
        {
            if(!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(AssetBundleDownloadAll)}.cs: " +
                    $"Failed to download bundle. " +
                    $"Message: {args.Error}.");
            }

            m_downloadCount++;

            OnDownloadProgress?.Invoke(m_downloadAmount, m_downloadCount);

            if(m_downloadCount >= m_downloadAmount)
            {
                Debug.Log($"{nameof(AssetBundleDownloadAll)}.cs: " +
                    $"All bundles are downloaded.");

                OnAllBundlesDownloaded?.Invoke();
            }
        }
    }
}