using UnityEditor;
using UnityEngine;

namespace KronosTech.Teleport.GizmosVisualizer
{
    [ExecuteInEditMode]
    public class GizmoTeleportLocation : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TeleportLocation m_teleport;

        private Mesh m_capsuleMesh;

        private void Awake()
        {
            m_capsuleMesh = Resources.Load<GameObject>("BasicPrimitives/Capsule").GetComponent<MeshFilter>().sharedMesh;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(m_capsuleMesh, transform.position, Quaternion.identity, Vector3.one);
#if UNITY_EDITOR
            Handles.color = Color.cyan;
            Handles.Label(transform.position + (Vector3.up * 1.5f), m_teleport.Data.GetFullName());
#endif
        }
    }
}