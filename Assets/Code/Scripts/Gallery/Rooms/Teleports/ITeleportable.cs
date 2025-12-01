using UnityEngine;

namespace KronosTech.Gallery.Rooms.Teleportation
{
    public interface ITeleportable
    {
        public void Teleport(Transform location);
    }
}