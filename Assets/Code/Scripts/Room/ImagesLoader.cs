using KronosTech.Data;
using KronosTech.Services;
using KronosTech.ShowroomGeneration.Room;
using UnityEngine;

namespace KronosTech.Room
{
    public class ImagesLoader : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DataRepositoryRoomData m_repository; 
        [SerializeField] private RoomDisplayImages m_display; 

        private void OnEnable()
        {
            AssetsLoader.OnBundlesDownload += LoadImagesCallback;
        }
        private void OnDisable()
        {
            AssetsLoader.OnBundlesDownload -= LoadImagesCallback;
        }

        private void LoadImagesCallback()
        {
            var imagesCount = m_repository.Data.Images.Length;
            var sprites = new RoomSpriteData[imagesCount];

            for (int i = 0; i < imagesCount; i++)
            {
                var image = m_repository.Data.Images[i];
                //sprites[i].Title = m_repository.Data.Images[i].title;
                //sprites[i].Sprite = ServiceLocator.Instance.GetWebImagesService().LoadImage(image.asset);
            }

            m_display.AddSprites(sprites);
        }
    }
}
