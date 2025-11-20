using UnityEngine;

namespace Ui.Hud
{
    public class InteractHintDisplay : MonoBehaviour
    {
        private void Awake()
        {
            //FindFirstObjectByType<PlayerInteractableObject>(FindObjectsInactive.Include).OnLookedAtInteractableObject += SetCrossairState;
        }

        private void SetCrossairState(bool lookingAtInteractable)
        {
            GetComponent<Animator>().SetBool("Looking At Interactable", lookingAtInteractable);
        }
    }
}