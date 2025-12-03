using KronosTech.Gallery.Rooms;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Generation.TagSelection
{
    public class TagSelector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GenerateShowroom m_builder;
        [SerializeField] private Transform m_parent;
        [SerializeField] private TagToggle m_togglePrefab;

        private readonly List<TagToggle> m_toggles = new();

        private void OnEnable()
        {
            m_builder.OnGenerationStart += () => SetInteractivity(false);
            m_builder.OnGenerationEnd += (state) => SetInteractivity(true);
        }
        private void OnDisable()
        {
            m_builder.OnGenerationStart -= () => SetInteractivity(false);
            m_builder.OnGenerationEnd -= (state) => SetInteractivity(true);
        }
        private void Awake()
        {
            foreach (RoomTagFlags tag in System.Enum.GetValues(typeof(RoomTagFlags)))
            {
                if(tag == RoomTagFlags.None)
                {
                    continue;
                }

                var toggle = Instantiate(m_togglePrefab, m_parent);
                toggle.Initialize(tag);

                m_toggles.Add(toggle);
            }
        }

        public RoomTagFlags GetTags()
        {
            var tags = new RoomTagFlags();

            foreach (var toggle in m_toggles)
            {
                if (toggle.GetTag(out var tag))
                {
                    tags |= tag;
                }
            }

            return tags;
        }

        private void SetInteractivity(bool interactable)
        {
            foreach (var toggle in m_toggles)
            {
                toggle.SetInteractability(interactable);
            }
        }
    }
}