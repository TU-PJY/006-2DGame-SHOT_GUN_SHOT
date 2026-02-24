using UnityEngine;

public static class Math_
{
    public static float CalcDistance(Vector2 A, Vector2 B)
    {
        return Mathf.Sqrt(Mathf.Pow(A.x - B.x, 2f) + Mathf.Pow(A.y - B.y, 2f));
    }

    public static float CalcDegrees(Vector2 A, Vector2 B)
    {
        return Mathf.Atan2(B.y - A.y, B.x - A.x) * Mathf.Rad2Deg;
    }

    public static float CalcRadians(Vector2 A, Vector2 B)
    {
        return Mathf.Atan2(B.y - A.y, B.x - A.x);
    }

    public static Vector2 NDirection(Vector2 A, Vector2 B)
    {
        return (A - B).normalized;
    }
    
    public static Vector2 AngleDirection(float radians)
    {
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public static Vector2 OffsetPosition(Vector2 src, Vector2 direction, float distance)
    {
        return src + direction * distance;
    }

    // t * deltTime이 1.f을 초과하면 1.f 이하로 클램핑하는 Lerp
    public static float Lerp(float src, float dst, float t)
    {
        var InputT = t;
        InputT = Mathf.Clamp(InputT, 0f, 1f);
        return Mathf.Lerp(src, dst, InputT);
    }

    // t * deltTime이 1.f을 초과하면 1.f 이하로 클램핑하는 Lerp
    public static Vector2 Lerp(Vector2 src, Vector2 dst, float t)
    {
        var InputT = t;
        InputT = Mathf.Clamp(InputT, 0f, 1f);
        return Vector2.Lerp(src, dst, InputT);
    }
}
