using System;
using UnityEngine;

namespace KronosTech.Gallery.Customization
{
    public static class GalleryEnvironment
    {
        private static Material[] s_skyboxes;
        private static AudioClip[] s_audioClips;

        public static Action<Material[]> OnAddSkyboxMaterials;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            s_skyboxes = Resources.LoadAll<Material>("Gallery/Customization/Skybox");
            s_audioClips = Resources.LoadAll<AudioClip>("Gallery/Customization/EnvironmentSounds");

            ReplaceEnvironment(0);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterInitialize()
        {
            OnAddSkyboxMaterials?.Invoke(s_skyboxes);
        }

        public static void ReplaceEnvironment(int index)
        {
            RenderSettings.skybox = s_skyboxes[index];

            GalleryBackgroundNoise.RequestNewAudioClip(s_audioClips[index]);

            DynamicGI.UpdateEnvironment();
        }
    }
}