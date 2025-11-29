using UnityEngine;

namespace KronosTech.Gallery.Customization 
{
    /// <summary>
    /// Used mostly to select random decorations or variants.
    /// </summary>
    public class SelectRandomChild : MonoBehaviour
    {
        private void OnEnable()
        {
            var choosenDecorationIndex = Random.Range(0, transform.childCount);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == choosenDecorationIndex);
            }
        }
    }
}