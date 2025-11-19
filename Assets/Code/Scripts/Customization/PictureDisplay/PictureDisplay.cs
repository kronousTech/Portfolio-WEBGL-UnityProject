using KronosTech.ShowroomGeneration;
using UnityEngine;

namespace KronosTech.Customization.Pictures
{
    public class PictureDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer m_renderer;

        private IPlaceablePieceBase m_tile;

        private void Awake()
        {
            m_tile = GetComponentInParent<IPlaceablePieceBase>(true);
            m_tile.OnPlacement += SetPictureCallback;
        }
        private void OnDestroy()
        {
            m_tile.OnPlacement -= SetPictureCallback;
        }

        public void SetPictureCallback()
        {
            m_renderer.sprite = PictureController.RequestPicture();
        }
    }
}

