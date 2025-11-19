using KronosTech.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentDisplay
{
    public abstract class ContentDisplayBase<T> : MonoBehaviour
    {
        [Header("References Base")]
        [SerializeField] protected DataRepositoryRoomData m_repository;
        [SerializeField] private TextMeshProUGUI m_indexDisplay;
        [SerializeField] private GameObject m_downloadingImageGO;
        [SerializeField] private GameObject m_buttonsHolder;
        [SerializeField] private Button m_buttonNext;
        [SerializeField] private Button m_buttonPrev;

        protected T[] Data;

        private int m_index;
        protected int Index
        {
            get => m_index;
            set
            {
                m_index = ((value % Data.Length) + Data.Length) % Data.Length;

                UpdateDisplay(m_index);
            }
        }
        protected virtual void OnEnable()
        {
            AssetsLoader.OnBundlesDownload += PrepareDisplayCallback;
            m_buttonNext.onClick.AddListener(NextCallback);
            m_buttonPrev.onClick.AddListener(PreviousCallback);
        }
        protected virtual void OnDisable()
        {
            AssetsLoader.OnBundlesDownload -= PrepareDisplayCallback;
            m_buttonNext.onClick.RemoveListener(NextCallback);
            m_buttonPrev.onClick.RemoveListener(PreviousCallback);
        }

        private void NextCallback() => Index++;
        private void PreviousCallback() => Index--;
        private void PrepareDisplayCallback()
        {
            LoadData();

            Index = 0;

            m_downloadingImageGO.SetActive(false);
            m_buttonsHolder.SetActive(Data.Length > 1);
        }
        private void UpdateDisplay(int index)
        {
            m_indexDisplay.text = Data.Length > 1 ? (m_index + 1).ToString() : string.Empty;

            ShowContent(index);
        }

        protected abstract void LoadData();
        protected abstract void ShowContent(int index);
    }
}