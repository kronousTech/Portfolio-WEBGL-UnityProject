using KronosTech.Data;
using KronosTech.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentDisplay
{
    public class ContentDisplayImages : ContentDisplayBase<RoomSpriteData>
    {
        [Header("References")]
        [SerializeField] private Image m_display;
        [SerializeField] private TextMeshProUGUI m_titleDisplay;

        #region ContentDisplayBase
        protected override void LoadData()
        {
            var imagesCount = m_repository.Data.Images.Length;

            Data = new RoomSpriteData[imagesCount];

            for (int i = 0; i < imagesCount; i++)
            {
                var imageData = m_repository.Data.Images[i];

                Data[i] = new RoomSpriteData(
                    title: imageData.title,
                    sprite: ServiceLocator.Instance.GetWebImagesService().LoadImage(imageData.asset));
            }
        }
        protected override void ShowContent(int index)
        {
            m_display.sprite = Data[index].Sprite;
            m_titleDisplay.text = Data[index].Title;
        }
        #endregion
    }
}