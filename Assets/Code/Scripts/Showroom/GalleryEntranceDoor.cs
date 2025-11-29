using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GalleryEntranceDoor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GenerateShowroom m_generator;

        private AudioSource _audioSource;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
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
            _animator.SetBool("Open", open);

            if(open)
            {
                _audioSource.Play();
            }
        }
    }
}