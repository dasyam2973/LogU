using UnityEngine;

public static class ValueExtensions
{
    public static Vector4 WithX(this Vector4 vector4, float x)
    {
        vector4.x = x;
        return vector4;
    }

    public static Color WithAlpha(this Color color, float a)
    {
        color.a = a;
        return color;
    }
}