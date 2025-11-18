using KronosTech.AssetManagement;
using KronosTech.Data;
using KronosTech.Services;
using UnityEngine;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomInfoImages : MonoBehaviour
    {
        private RoomSpriteData[] _imageSprites;

        [SerializeField] private ContentData[] _imageData;
        [SerializeField] private RoomDisplayImages[] _displays;

        private void OnEnable()
        {
            AssetsLoader.OnBundlesDownload += LoadImages;   
        }
        private void OnDisable()
        {
            AssetsLoader.OnBundlesDownload -= LoadImages;
        }

        private void LoadImages()
        {
            _imageSprites = new RoomSpriteData[_imageData.Length];

            for (int i = 0; i < _imageData.Length; i++)
            {
                //_imageSprites[i].Title = _imageData[i].title;   
                //_imageSprites[i].Sprite = ServiceLocator.Instance.GetWebImagesService().LoadImage(_imageData[i].asset);
            }

            AddSpritesToDisplays();
        }

        private void AddSpritesToDisplays()
        {
            for (int i = 0; i < _displays.Length; i++)
            {
                _displays[i].AddSprites(_imageSprites);
            }
        }
    }
}