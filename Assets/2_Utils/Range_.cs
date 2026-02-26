using UnityEngine;

public static class Range_
{
    public static bool InRange(int val, int rangeMin, int rangeMax)
    {
        return rangeMin <= val && val <= rangeMax;
    }

    public static bool Propability(int percentage)
    {
        var randVal = Random.Range(1, 101);
        return 1 <= randVal && randVal <= percentage;
    }
}
