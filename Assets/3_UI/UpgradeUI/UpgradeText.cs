using System.Text;
using UnityEngine;
using UnityEngine.UI;

enum LevelItem
{
    NA,
    Armor,
    Pellet,
    ShootSpeed,
    ReloadSpeed,
    NearAttackDmg
}

public class UpgradeText : UpgradeItem
{
    public Text text;

    private string originString;
    
    void Start()
    {
        UpdateText();
    }

    void Awake()
    {
        originString = text.text;
    }

    public void UpdateText()
    {
        LevelItem item = LevelItem.NA;

        if (usingArmorLevel)
            item = LevelItem.Armor;
        else if(usingPelletLevel)
            item = LevelItem.Pellet;
        else if(usingShootSpeedLevel)
            item = LevelItem.ShootSpeed;
        else if(usingReloadSpeedLevel)
            item = LevelItem.ReloadSpeed;
        else if(usingNearAttackDmgLevel)
            item = LevelItem.NearAttackDmg;

        var modified = originString;

        // 선택한 항목에 따라 다른 값을 얻어온다.
        switch (item)
        {
            case LevelItem.Armor:
                modified = modified.Replace("{1}", St_LevelManager.Inst.armorDiffPercentage.ToString());
                modified = modified.Replace("{2}", St_LevelManager.Inst.armorLevel.ToString());
                modified = modified.Replace("{3}", St_LevelManager.Inst.armorLevelLimit.ToString());
                break;

            case LevelItem.Pellet:
                modified = modified.Replace("{1}", St_LevelManager.Inst.pelletDiffIncrease.ToString());
                modified = modified.Replace("{2}", St_LevelManager.Inst.pelletLevel.ToString());
                modified = modified.Replace("{3}", St_LevelManager.Inst.pelletLevelLimit.ToString());
                break;

            case LevelItem.ShootSpeed:
                modified = modified.Replace("{1}", St_LevelManager.Inst.shootSpeedDiffPercentage.ToString());
                modified = modified.Replace("{2}", St_LevelManager.Inst.shootSpeedLevel.ToString());
                modified = modified.Replace("{3}", St_LevelManager.Inst.shootSpeedLevelLimit.ToString());
                break;

            case LevelItem.ReloadSpeed:
                modified = modified.Replace("{1}", St_LevelManager.Inst.reloadSpeedDiffPercentage.ToString());
                modified = modified.Replace("{2}", St_LevelManager.Inst.reloadSpeedLevel.ToString());
                modified = modified.Replace("{3}", St_LevelManager.Inst.reloadSpeedLevelLimit.ToString());
                break;

            case LevelItem.NearAttackDmg:
                modified = modified.Replace("{1}", St_LevelManager.Inst.nearAttackDmgDiffPercentage.ToString());
                modified = modified.Replace("{2}", St_LevelManager.Inst.nearAttackDmgLevel.ToString());
                modified = modified.Replace("{3}", St_LevelManager.Inst.nearAttackDmgLevelLimit.ToString());
                break;
        }

        text.text = modified;
    }
}
