using KronosTech.Gallery.Rooms.ContentViewer;
using TMPro;
using UnityEngine;

namespace KronosTech.Gallery.Rooms.ContentViewer.Presenter
{
    public class ContentViewerTitleAndIndexPresenter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ContentViewerBase m_contentViewer;
        [SerializeField] private TextMeshProUGUI m_title;
        [SerializeField] private TextMeshProUGUI m_index;

        private void OnEnable()
        {
            m_contentViewer.OnAssetChange += DisplayTitleAndIndexCallback;
        }
        private void OnDisable()
        {
            m_contentViewer.OnAssetChange -= DisplayTitleAndIndexCallback;
        }

        private void DisplayTitleAndIndexCallback(ContentViewerOnAssetChangeEventArgs args)
        {
            m_title.text = args.Data.Title;
            m_index.text = (args.Index +1).ToString();
        }
    }
}