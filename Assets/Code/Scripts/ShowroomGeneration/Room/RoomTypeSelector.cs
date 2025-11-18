using KronosTech.Data;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    [ExecuteInEditMode]
    public class RoomTypeSelector : MonoBehaviour
    {
        [SerializeField] private Transform _typesParent;
        [SerializeField] private RoomType _type;

        private void Awake()
        {
            for (int i = 0; i < _typesParent.childCount; i++)
            {
                _typesParent.GetChild(i).gameObject.SetActive(i == (int)_type);
            }
        }
    }
}