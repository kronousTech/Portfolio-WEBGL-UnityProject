using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Customization
{
    public class GalleryMaterialsSelector : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private MaterialButtonDisplay m_buttonPrefab;
        [Header("Parents")]
        [SerializeField] private ToggleGroup m_floorParent;
        [SerializeField] private ToggleGroup m_baseboardParent;
        [SerializeField] private ToggleGroup m_wallParent;
        [SerializeField] private ToggleGroup m_guidelinesParent;

        private void OnEnable()
        {
            GalleryCustomization.OnAddFloorMaterials += (materials) => AddButtons(CustomizableType.Floor, m_floorParent, materials);
            GalleryCustomization.OnAddBaseboardMaterials += (materials) => AddButtons(CustomizableType.Baseboard, m_baseboardParent, materials);
            GalleryCustomization.OnAddWallMaterials += (materials) => AddButtons(CustomizableType.Wall, m_wallParent, materials);
            GalleryCustomization.OnAddGuidelineMaterials += (materials) => AddButtons(CustomizableType.Guideline, m_guidelinesParent, materials);
        }
        private void OnDisable()
        {
            GalleryCustomization.OnAddFloorMaterials -= (materials) => AddButtons(CustomizableType.Floor, m_floorParent, materials);
            GalleryCustomization.OnAddBaseboardMaterials -= (materials) => AddButtons(CustomizableType.Baseboard, m_baseboardParent, materials);
            GalleryCustomization.OnAddWallMaterials -= (materials) => AddButtons(CustomizableType.Wall, m_wallParent, materials);
            GalleryCustomization.OnAddGuidelineMaterials -= (materials) => AddButtons(CustomizableType.Guideline, m_guidelinesParent, materials);
        }

        private void AddButtons(CustomizableType element, ToggleGroup parent, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                var toggle = Instantiate(m_buttonPrefab, parent.transform);
                toggle.Initialize(material);
                toggle.Toggle.group = parent;
                toggle.Toggle.onValueChanged.AddListener((value) =>
                {
                    if (value)
                    {
                        GalleryCustomization.SetNewCurrentMat(element, material);
                    }
                });
            }
        }
    }
}