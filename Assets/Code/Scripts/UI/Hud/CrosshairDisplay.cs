using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Ui.Hud
{
    public class CrosshairDisplay : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule m_inputModule;


        private void SetCrossairState(bool lookingAtInteractable)
        {
            GetComponent<Animator>().SetBool("Looking At Interactable", lookingAtInteractable);
        }
    }

}