using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Customization
{
    public class GalleryCustomization
    {
        private static readonly Dictionary<CustomizableType, Material[]> s_materialsDict = new();
        private static readonly Dictionary<CustomizableType, Material> s_selectedMaterialsDict = new();

        private static readonly List<CustomizableRenderer> s_renderers = new();
        private static readonly List<CustomizableDecal> s_decals = new();

        public static Action<Material[]> OnAddFloorMaterials;
        public static Action<Material[]> OnAddBaseboardMaterials;
        public static Action<Material[]> OnAddWallMaterials;
        public static Action<Material[]> OnAddGuidelineMaterials;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            s_materialsDict.Add(CustomizableType.Floor, 
                Resources.LoadAll<Material>("Gallery/Customization/Floor"));
            s_materialsDict.Add(CustomizableType.Baseboard, 
                Resources.LoadAll<Material>("Gallery/Customization/Baseboard"));
            s_materialsDict.Add(CustomizableType.Wall, 
                Resources.LoadAll<Material>("Gallery/Customization/Wall"));
            s_materialsDict.Add(CustomizableType.Guideline, 
                Resources.LoadAll<Material>("Gallery/Customization/Guideline"));

            s_selectedMaterialsDict.Add(CustomizableType.Floor, 
                s_materialsDict[CustomizableType.Floor][0]);
            s_selectedMaterialsDict.Add(CustomizableType.Baseboard, 
                s_materialsDict[CustomizableType.Baseboard][0]);
            s_selectedMaterialsDict.Add(CustomizableType.Wall, 
                s_materialsDict[CustomizableType.Wall][0]);
            s_selectedMaterialsDict.Add(CustomizableType.Guideline, 
                s_materialsDict[CustomizableType.Guideline][0]);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterInitialize() 
        {
            OnAddFloorMaterials?.Invoke(s_materialsDict[CustomizableType.Floor]);
            OnAddBaseboardMaterials?.Invoke(s_materialsDict[CustomizableType.Baseboard]);
            OnAddWallMaterials?.Invoke(s_materialsDict[CustomizableType.Wall]);
            OnAddGuidelineMaterials?.Invoke(s_materialsDict[CustomizableType.Guideline]);
        }

        public static void AddCustomizableRenderer(CustomizableRenderer renderer)
        {
            s_renderers.Add(renderer);

            foreach (var item in s_selectedMaterialsDict)
            {
                renderer.ReplaceMaterial(item.Key.ToString(), item.Value);
            }
        }
        public static void RemoveCustomizableRenderer(CustomizableRenderer renderer)
        {
            if (s_renderers.Contains(renderer))
            {
                s_renderers.Remove(renderer);
            }
        }
        public static void AddCustomizableDecal(CustomizableDecal decal)
        {
            s_decals.Add(decal);

            decal.ReplaceMaterial(s_selectedMaterialsDict[CustomizableType.Guideline]);
        }
        public static void RemoveCustomizableDecal(CustomizableDecal decal)
        {
            if (s_decals.Contains(decal))
            {
                s_decals.Remove(decal);
            }
        }

        private static void UpdateMat(CustomizableType element, Material material)
        {
            if(element == CustomizableType.Guideline)
            {
                foreach (var item in s_decals)
                {
                    item.ReplaceMaterial(material);
                }
            }
            else
            {
                foreach (var item in s_renderers)
                {
                    item.ReplaceMaterial(element.ToString(), material);
                }
            }
        }

        public static void SetNewCurrentMat(CustomizableType element, Material material)
        {
            s_selectedMaterialsDict[element] = material;

            UpdateMat(element, s_selectedMaterialsDict[element]);
        }
        public static void SetNewCurrentMat(CustomizableType element, int index)
        {
            s_selectedMaterialsDict[element] = s_materialsDict[element][index];

            UpdateMat(element, s_selectedMaterialsDict[element]);
        }
    }
}