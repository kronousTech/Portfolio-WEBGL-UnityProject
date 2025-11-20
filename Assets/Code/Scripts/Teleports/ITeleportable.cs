using UnityEngine;

namespace KronosTech.Teleport
{
    public interface ITeleportable
    {
        public void Teleport(Transform location);
    }
}