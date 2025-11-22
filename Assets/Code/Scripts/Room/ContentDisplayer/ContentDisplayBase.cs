using KronosTech.AssetBundles;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentDisplay
{
    public abstract class ContentDisplayBase<T> : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected ContentDataArrayData m_data;
        [Header("References Base")]
        [SerializeField] private TextMeshProUGUI m_indexDisplay;
        [SerializeField] private GameObject m_downloadingImageGO;
        [SerializeField] private GameObject m_buttonsHolder;
        [SerializeField] private Button m_buttonNext;
        [SerializeField] private Button m_buttonPrev;

        private AssetBundleDownloadAll m_downloader;

        protected List<T> Data;

        private int m_index;
        protected int Index
        {
            get => m_index;
            set
            {
                if(Data.Count == 0)
                {
                    m_index = 0;
                }
                else
                {
                    m_index = ((value % Data.Count) + Data.Count) % Data.Count;
                }

                UpdateDisplay();
            }
        }
        private void Awake()
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

        public void SetData(ContentDataArrayData data)
        {
            m_data = data;

            PrepareDisplay();
        }
        private void LoadContentDataCallback()
        {
            if (m_data != null)
            {
                PrepareDisplay();
            }
        }

        private void NextCallback() => Index++;
        private void PreviousCallback() => Index--;
        private void PrepareDisplay()
        {
            LoadData(() =>
            {
                Index = 0;

                m_downloadingImageGO.SetActive(false);
                m_buttonsHolder.SetActive(Data.Count > 1);
            });
        }
        private void UpdateDisplay()
        {
            m_indexDisplay.text = Data.Count > 1 ? (m_index + 1).ToString() : string.Empty;

            ShowContent(m_index);
        }

        protected abstract void LoadData(Action callback);
        protected abstract void ShowContent(int index);
    }
}