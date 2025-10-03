using UnityEngine;

public static class UIExtensions
{
    public static float AnchoredLeft(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition.x - rectTransform.rect.size.x * rectTransform.pivot.x;
    }
    public static float AnchoredRight(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition.x + rectTransform.rect.size.x * (1 - rectTransform.pivot.x);
    }
    public static float AnchoredTop(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition.y + rectTransform.rect.size.y * (1 - rectTransform.pivot.y);
    }
    public static float AnchoredBottom(this RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition.y - rectTransform.rect.size.y * rectTransform.pivot.y;
    }

    public static void SetAnchoredX(this RectTransform rectTransform, float x)
    {
        rectTransform.anchoredPosition = new(x, rectTransform.anchoredPosition.y);
    }
    public static void SetAnchoredY(this RectTransform rectTransform, float y)
    {
        rectTransform.anchoredPosition = new(rectTransform.anchoredPosition.x, y);
    }

    public static void ClampAnchoredPositionToRect(this RectTransform rectTransform, Rect rect)
    {
        float minX = rect.xMin + rectTransform.rect.size.x * rectTransform.pivot.x;
        float maxX = rect.xMax - rectTransform.rect.size.x * (1 - rectTransform.pivot.x);

        float minY = rect.yMin + rectTransform.rect.size.y * rectTransform.pivot.y;
        float maxY = rect.yMax - rectTransform.rect.size.y * (1 - rectTransform.pivot.y);

        rectTransform.anchoredPosition = new(x: Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX),
                                             y: Mathf.Clamp(rectTransform.anchoredPosition.y, minY, maxY));
    }
    public static void ClampPositionToBounds(this RectTransform child, RectTransform target)
    {
        float minX = -target.rect.size.x * target.pivot.x + child.rect.size.x * child.pivot.x;
        float maxX = target.rect.size.x * (1 - target.pivot.x) - child.rect.size.x * (1 - child.pivot.x);

        float minY = -target.rect.size.y * target.pivot.y + child.rect.size.y * child.pivot.y;
        float maxY = target.rect.size.y * (1 - target.pivot.y) - child.rect.size.y * (1 - child.pivot.y);

        child.anchoredPosition = new(x: Mathf.Clamp(child.anchoredPosition.x, minX, maxX),
                                     y: Mathf.Clamp(child.anchoredPosition.y, minY, maxY));
    }
}