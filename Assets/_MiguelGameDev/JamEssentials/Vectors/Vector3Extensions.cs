using UnityEngine;

namespace MiguelGameDev
{
    public static class Vector3Extensions
    {
        public static Vector2 XZToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z); 
        }
    }
}
