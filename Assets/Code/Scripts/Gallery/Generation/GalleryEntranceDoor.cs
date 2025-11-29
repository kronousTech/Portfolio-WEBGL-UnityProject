using UnityEngine;

namespace KronosTech.Gallery.Generation
{
    public class GalleryEntranceDoor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GenerateShowroom m_generator;

        private AudioSource m_audioSource;
        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            m_generator.OnGenerationStart += () => SetDoorState(false);
            m_generator.OnGenerationEnd += (state) => SetDoorState(state);
        }
        private void OnDisable()
        {
            m_generator.OnGenerationStart -= () => SetDoorState(false);
            m_generator.OnGenerationEnd -= (state) => SetDoorState(state);
        }

        private void SetDoorState(bool open)
        {
            m_animator.SetBool("Open", open);

            if(open)
            {
                m_audioSource.Play();
            }
        }
    }
}