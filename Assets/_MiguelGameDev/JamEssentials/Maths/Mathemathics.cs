using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev
{
    public static class Mathemathics
    {
        public static float Diff(float a, float b)
        {
            return Mathf.Abs(a - b);
        }
    }
}