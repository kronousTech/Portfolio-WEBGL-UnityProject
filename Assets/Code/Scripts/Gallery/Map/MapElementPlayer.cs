using KronosTech.InputSystem;
using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementPlayer : MapElementSource
    {
        private void OnEnable()
        {
            StarterAssetsInputs.OnMoveEvent += UpdateMapPositionCallback;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            StarterAssetsInputs.OnMoveEvent -= UpdateMapPositionCallback;
        }
        private void Start()
        {
            UpdateMapPosition();
        }

        private void UpdateMapPositionCallback(bool value)
        {
            // We are updating every time the player stops.
            if(!value)
            {
                UpdateMapPosition();
            }
        }
    }
}