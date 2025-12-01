using UnityEngine;

namespace KronosTech.Gallery.Customization.Decoration
{
    public class DecorationModel : MonoBehaviour
    {
        private void Awake()
        {
            DecorationController.Add(this);
        }
        private void OnDestroy()
        {
            DecorationController.Remove(this);
        }
    }
}