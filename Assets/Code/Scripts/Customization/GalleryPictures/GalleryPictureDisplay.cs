using KronosTech.ShowroomGeneration;
using UnityEngine;

namespace KronosTech.Customization.GalleryPictures
{
    public class GalleryPictureDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer m_renderer;

        private LoaderGalleryPictures m_loader;
        private IPlaceablePieceBase m_tile;

        private void Awake()
        {
            m_loader = FindFirstObjectByType<LoaderGalleryPictures>(FindObjectsInactive.Include);
            m_tile = GetComponentInParent<IPlaceablePieceBase>(true);
            m_tile.OnPlacement += SetPictureCallback;
        }
        private void OnDestroy()
        {
            m_tile.OnPlacement -= SetPictureCallback;
        }

        public void SetPictureCallback()
        {
            m_renderer.sprite = m_loader.RequestPicture();
        }
    }
}

