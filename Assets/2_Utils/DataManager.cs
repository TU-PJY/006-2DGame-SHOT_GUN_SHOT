using System;
using UnityEngine;

// 플레이어 데이터를 관리하는 모듈.
public static class PlayerData
{
    public static int LoadBestRoundsData()
    {
        return PlayerPrefs.GetInt("SGS_BestRounds");
    }

    public static void UpdateBestRoundsData(int val)
    {
        var prevVal = PlayerPrefs.GetInt("SGS_BestRounds");
        PlayerPrefs.SetInt("SGS_BestRounds", val);
        Debug.Log($"[PlayerData] Best rounds data updated. {prevVal} -> {val}");
    }
}