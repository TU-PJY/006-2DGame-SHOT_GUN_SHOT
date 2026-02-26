using System.Collections.Generic;
using UnityEngine;

public class St_LevelManager : MonoBehaviour
{
    public static St_LevelManager Inst;
    
    public int armorLevel;
    public int armorLevelLimit;
    
    public int pelletLevel;
    public int pelletLevelLimit;

    public int shootSpeedLevel;
    public int shootSpeedLevelLimit;

    public int reloadSpeedLevel;
    public int reloadSpeedLevelLimit;

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
            armorLevel ++;
    }

    public void IncreasePelletLevel()
    {
        if (pelletLevel < pelletLevelLimit)
            pelletLevel ++;
    }

    public void IncreaseShootSpeedLevel()
    {
        if (shootSpeedLevel < shootSpeedLevelLimit)
            shootSpeedLevel++;
    }

    public void IncreaseReloadSpeedLevel()
    {
        if (reloadSpeedLevel < reloadSpeedLevelLimit)
            reloadSpeedLevel++;
    }
}
