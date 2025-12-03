using UnityEngine;

namespace KronosTech.Gallery.Customization.Environment
{
    [CreateAssetMenu(
        fileName = "EnvironmentData", 
        menuName = "Scriptable Objects/Environment Data")]
    public class EnvironmentData : ScriptableObject
    {
        public Sprite DisplayIcon;
        public Material SkyBoxMaterial;
        public float IntensityMultiplier = 1;
        public float CameraAngleY;
        public AudioClip BackgroundNoise;
    }
}