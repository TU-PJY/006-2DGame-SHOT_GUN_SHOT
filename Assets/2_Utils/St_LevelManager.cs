using System.Collections.Generic;
using UnityEngine;

// level: 현재 레벨
// levelLimit: 최대 레벨
// levelDiff: 현재 레벨에 의한 변화 비율 // 고정 값에 곱하여 변화 가함

public class St_LevelManager : MonoBehaviour
{
    public static St_LevelManager Inst;
    
    public int armorLevel;
    public int armorLevelLimit;
    public float armorDiffValue;
    [HideInInspector]
    public float armorDiff = 1f;
    
    public int pelletLevel;
    public int pelletLevelLimit;
     [HideInInspector]
    public int pelletDiff = 1;

    public int shootSpeedLevel;
    public int shootSpeedLevelLimit;
    public float shootSpeedDiffValue;
     [HideInInspector]
    public float shootSpeedDiff = 1f;

    public int reloadSpeedLevel;
    public int reloadSpeedLevelLimit;
    public float reloadSpeedDiffValue;
     [HideInInspector]
    public float reloadSpeedDiff = 1f;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[LevelManager] Created instance.");
    }

    public void Release()
    {
        print("[LevelManager] Released instance.");
        Inst = null;
    }

    public void IncreaseArmorLevel()
    {
        if (armorLevel < armorLevelLimit)
        {
            armorLevel++;
            armorDiff *= armorDiffValue;
        }
    }

    public void IncreasePelletLevel()
    {
        if (pelletLevel < pelletLevelLimit)
        {
            pelletLevel++;
            pelletDiff += 1;
        }
    }

    public void IncreaseShootSpeedLevel()
    {
        if (shootSpeedLevel < shootSpeedLevelLimit)
        {
            shootSpeedLevel++;
            shootSpeedDiff *= shootSpeedDiffValue;
        }
    }

    public void IncreaseReloadSpeedLevel()
    {
        if (reloadSpeedLevel < reloadSpeedLevelLimit) {
            reloadSpeedLevel++;
            reloadSpeedDiff *= reloadSpeedDiffValue;
        }
    }
}
