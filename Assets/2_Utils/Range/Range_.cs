using UnityEngine;

public static class Range_
{
    public static bool InRange(int val, int rangeMin, int rangeMax)
    {
        return rangeMin <= val && val <= rangeMax;
    }
}
