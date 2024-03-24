using UnityEngine;

namespace MiguelGameDev
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3XZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}
