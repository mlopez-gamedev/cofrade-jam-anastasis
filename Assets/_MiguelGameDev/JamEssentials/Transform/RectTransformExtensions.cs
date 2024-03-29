using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetAnchoredPositionX(this RectTransform t, float x)
    {
        Vector2 position = t.anchoredPosition;
        position = new Vector2(x, position.y);
        t.anchoredPosition = position;
    }

    public static void SetAnchoredPositionY(this RectTransform t, float y)
    {
        Vector2 position = t.anchoredPosition;
        position = new Vector2(position.x, y);
        t.anchoredPosition = position;
    }

    public static void SetAnchorMinX(this RectTransform t, float x)
    {
        var anchorMin = t.anchorMin;
        anchorMin = new Vector2(x, anchorMin.y);
        t.anchorMin = anchorMin;
    }

    public static void SetAnchorMinY(this RectTransform t, float y)
    {
        var anchorMin = t.anchorMin;
        anchorMin = new Vector2(anchorMin.x, y);
        t.anchorMin = anchorMin;
    }

    public static void SetAnchorMaxX(this RectTransform t, float x)
    {
        var anchorMax = t.anchorMax;
        anchorMax = new Vector2(x, anchorMax.y);
        t.anchorMax = anchorMax;
    }

    public static void SetAnchorMaxY(this RectTransform t, float y)
    {
        var anchorMax = t.anchorMax;
        anchorMax = new Vector2(anchorMax.x, y);
        t.anchorMax = anchorMax;
    }

    public static void SetAnchorX(this RectTransform t, float x)
    {
        var anchorMin = t.anchorMin;
        var anchorMax = t.anchorMax;
        anchorMin = new Vector2(x, anchorMin.y);
        anchorMax = new Vector2(x, anchorMax.y);
        t.anchorMin = anchorMin;
        t.anchorMax = anchorMax;
    }

    public static void SetAnchorY(this RectTransform t, float y)
    {
        var anchorMin = t.anchorMin;
        var anchorMax = t.anchorMax;
        anchorMin = new Vector2(anchorMin.x, y);
        anchorMax = new Vector2(anchorMax.x, y);
        t.anchorMin = anchorMin;
        t.anchorMax = anchorMax;
    }

    public static void SetSizeDelta(this RectTransform t, float size)
    {
        t.sizeDelta = new Vector2(size, size);
    }
}
