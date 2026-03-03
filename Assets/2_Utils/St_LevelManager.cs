using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

// level: 현재 레벨
// levelLimit: 최대 레벨
// levelDiff: 현재 레벨에 의한 변화 비율 // 고정 값에 곱하여 변화 가함

public class St_LevelManager : MonoBehaviour
{
    public static St_LevelManager Inst;

    public int armorLevel;
    public int armorLevelLimit;
    public int armorDiffPercentage;
    [HideInInspector]
    public float armorDiff = 1f;

    public int pelletLevel;
    public int pelletLevelLimit;
    public int pelletDiffIncrease;
    [HideInInspector]
    public int pelletDiff = 1;

    public int shootSpeedLevel;
    public int shootSpeedLevelLimit;
    public float shootSpeedDiffPercentage;
    [HideInInspector]
    public float shootSpeedDiff = 1f;

    public int reloadSpeedLevel;
    public int reloadSpeedLevelLimit;
    public float reloadSpeedDiffPercentage;
    [HideInInspector]
    public float reloadSpeedDiff = 1f;

    public int nearAttackDmgLevel;
    public int nearAttackDmgLevelLimit;
    public float nearAttackDmgDiffPercentage;
    [HideInInspector]
    public float nearAttackDmgDiff = 1f;

    [HideInInspector] // 모든 항목이 최고 레벨일 경우 true
    public bool isAllLevelMax = false;

    void Awake()
    {
        if (Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[LevelManager] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    public void IncreaseArmorLevel()
    {
        if (armorLevel < armorLevelLimit)
        {
            armorLevel++;
            var mul = armorDiffPercentage / 100f;
            var currentDiff = armorDiff + armorDiff * mul;
            armorDiff = currentDiff;
            CheckAllLevelMax();
        }
    }

    public void IncreasePelletLevel()
    {
        if (pelletLevel < pelletLevelLimit)
        {
            pelletLevel++;
            pelletDiff += pelletDiffIncrease;
            CheckAllLevelMax();
        }
    }

    public void IncreaseShootSpeedLevel()
    {
        if (shootSpeedLevel < shootSpeedLevelLimit)
        {
            shootSpeedLevel++;
            var mul = shootSpeedDiffPercentage / 100f;
            var currentDiff = shootSpeedDiff + shootSpeedDiff * mul;
            shootSpeedDiff = currentDiff;
            CheckAllLevelMax();
        }
    }

    public void IncreaseReloadSpeedLevel()
    {
        if (reloadSpeedLevel < reloadSpeedLevelLimit)
        {
            reloadSpeedLevel++;
            var mul = shootSpeedDiffPercentage / 100f;
            var currentDiff = reloadSpeedDiff + reloadSpeedDiff * mul;
            reloadSpeedDiff = currentDiff;
            CheckAllLevelMax();
        }
    }

    public void IncreaseNearAttackDmgLevel()
    {
        if (nearAttackDmgLevel < nearAttackDmgLevelLimit)
        {
            nearAttackDmgLevel++;
              var mul = shootSpeedDiffPercentage / 100f;
            var currentDiff = nearAttackDmgDiff + nearAttackDmgDiff * mul;
            nearAttackDmgDiff = currentDiff;
            CheckAllLevelMax();
        }
    }

    void CheckAllLevelMax()
    {
        bool isArmorMax = armorLevel == armorLevelLimit;
        bool isPelletMax = pelletLevel == pelletLevelLimit;
        bool isShootSpeedMax = shootSpeedLevel == shootSpeedLevelLimit;
        bool isReloadSpeedMax = reloadSpeedLevel == reloadSpeedLevelLimit;
        bool isNearAttackDmgMax = nearAttackDmgLevel == nearAttackDmgLevelLimit;
        isAllLevelMax = isArmorMax && isPelletMax && isShootSpeedMax && isReloadSpeedMax && isNearAttackDmgMax;
    }
}
