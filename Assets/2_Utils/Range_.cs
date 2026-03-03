using System;
using System.Security.Cryptography;
using UnityEngine;

public static class Range_
{
    public static bool InRange(int val, int rangeMin, int rangeMax)
    {
        return rangeMin <= val && val <= rangeMax;
    }

    public static bool Probability(int percentage)
    {
        int max = 100;
        int min = 1;
        byte[] bytes = new byte[4];
        RandomNumberGenerator.Fill(bytes);
        int rawValue = BitConverter.ToInt32(bytes, 0);
        uint uValue = unchecked((uint)rawValue);
        uint range = (uint)(max - min + 1);
        int result = (int)(uValue % range) + min;
        return InRange(result, min, percentage);
    }
}
