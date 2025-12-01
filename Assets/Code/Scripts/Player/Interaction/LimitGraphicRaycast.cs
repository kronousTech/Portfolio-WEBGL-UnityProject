using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KronosTech.Player.Interaction
{
    public class LimitGraphicRaycast : GraphicRaycaster
    {
        private const float k_maxDistance = 4f;

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            base.Raycast(eventData, resultAppendList);

            for (int i = resultAppendList.Count - 1; i >= 0; i--)
            {
                if(resultAppendList[i].distance > k_maxDistance)
                {
                    resultAppendList.RemoveAt(i);
                }
            }
        }
    }
}