using UnityEngine;

public static class Mathv
{
    public static float CalcDistance(Vector2 A, Vector2 B)
    {
        return Mathf.Sqrt(Mathf.Pow(A.x - B.x, 2f) + Mathf.Pow(A.y - B.y, 2f));
    }

    public static float CalcDegrees(Vector2 A, Vector2 B)
    {
        return Mathf.Atan2(B.y - A.y, B.x - A.x) * Mathf.Rad2Deg;
    }

    public static Vector2 NDirection(Vector2 A, Vector2 B)
    {
        return (A - B).normalized;
    }
}
