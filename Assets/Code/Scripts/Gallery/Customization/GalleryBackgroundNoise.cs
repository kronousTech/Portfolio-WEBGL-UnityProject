using System;
using UnityEngine;

namespace KronosTech.Gallery.Customization
{
    public class GalleryBackgroundNoise : MonoBehaviour
    {
        private AudioSource m_source;

        private static event Action<AudioClip> OnNewAudioClipRequest;

        private void OnEnable()
        {
            OnNewAudioClipRequest += ReplaceAudio;
        }
        private void OnDisable()
        {
            OnNewAudioClipRequest -= ReplaceAudio;
        }
        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        public static void RequestNewAudioClip(AudioClip audioClip) => OnNewAudioClipRequest?.Invoke(audioClip);

        private void ReplaceAudio(AudioClip clip)
        {
            m_source.clip = clip;
            m_source.Play();
        }
    }
}