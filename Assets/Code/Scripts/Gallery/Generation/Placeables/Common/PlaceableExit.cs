using UnityEngine;

namespace KronosTech.Gallery.Generation.Placeables
{
    public class PlaceableExit : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool m_addLines = true;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public bool AddLines => m_addLines;
    }
}