using KronosTech.AssetBundles;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentViewer
{
    public abstract class ContentViewerBase : MonoBehaviour
    {
        public Action<ContentViewerOnAssetChangeEventArgs> OnAssetChange;
    }
    public abstract class ContentViewerBase<T> : ContentViewerBase where T : ContentData
    {
        [Header("Settings")]
        [SerializeField] protected DataArrayHolder<T> m_dataHolder;
        [Header("References Base")]
        [SerializeField] private GameObject m_downloadingImageGO;
        [SerializeField] private GameObject m_buttonsHolder;
        [SerializeField] private Button m_buttonNext;
        [SerializeField] private Button m_buttonPrev;

        private AssetBundleDownloadAll m_downloader;

        private int m_index;
        protected int Index
        {
            get => m_index;
            set
            {
                if(m_dataHolder.Data.Length == 0)
                {
                    m_index = 0;
                }
                else
                {
                    m_index = ((value % m_dataHolder.Data.Length) + m_dataHolder.Data.Length) % m_dataHolder.Data.Length;
                }

                ShowContent(m_dataHolder.Data[m_index] as T);

                OnAssetChange?.Invoke(new(m_index, m_dataHolder.Data[m_index]));
            }
        }

        protected virtual void Awake()
        {
            m_downloader = FindFirstObjectByType<AssetBundleDownloadAll>(FindObjectsInactive.Include);
        }

        protected virtual void OnEnable()
        {
            m_downloader.OnAllBundlesDownloaded += LoadContentDataCallback;
            m_buttonNext.onClick.AddListener(NextCallback);
            m_buttonPrev.onClick.AddListener(PreviousCallback);
        }
        protected virtual void OnDisable()
        {
            m_downloader.OnAllBundlesDownloaded -= LoadContentDataCallback;
            m_buttonNext.onClick.RemoveListener(NextCallback);
            m_buttonPrev.onClick.RemoveListener(PreviousCallback);
        }

        public void SetData(DataArrayHolder<T> data)
        {
            if(data == null)
            {
                Debug.LogError($"{nameof(ContentViewerBase<ContentData>)}.cs: " +
                    $"Data is null.");

                return;
            }

            m_dataHolder = data;

            PrepareDisplay();
        }
        private void LoadContentDataCallback()
        {
            if (m_dataHolder != null)
            {
                PrepareDisplay();
            }
        }

        private void NextCallback() => Index++;
        private void PreviousCallback() => Index--;
        private void PrepareDisplay()
        {
            Index = 0;

            m_downloadingImageGO.SetActive(false);
            m_buttonsHolder.SetActive(m_dataHolder.Data.Length > 1);
        }
        private void UpdateDisplay()
        {
            //m_indexDisplay.text = m_dataHolder.Data.Length > 1 ? (m_index + 1).ToString() : string.Empty;
        }

        protected abstract void ShowContent(T content);
    }
}