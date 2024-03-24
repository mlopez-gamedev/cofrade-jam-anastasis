using UnityEngine;
using UnityEngine.UI;

namespace MiguelGameDev
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this Graphic renderer, float alpha)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        }
    }
}