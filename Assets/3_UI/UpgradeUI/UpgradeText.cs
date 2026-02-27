using UnityEngine;
using UnityEngine.UI;

enum LevelItem
{
    NA,
    Armor,
    Pellet,
    ShootSpeed,
    ReloadSpeed
}

public class UpgradeText : UpgradeItem
{
    public Text text;

    [TextArea(8, 8)]
    public string textContent;
    public int level;
    
    void Start()
    {
        text = GetComponent<Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        int currLevel = 0;
        int maxLevel = 0;
        LevelItem item = LevelItem.NA;

        if (usingArmorLevel)
            item = LevelItem.Armor;
        else if(usingPelletLevel)
            item = LevelItem.Pellet;
        else if(usingShootSpeedLevel)
            item = LevelItem.ShootSpeed;
        else if(usingReloadSpeedLevel)
            item = LevelItem.ReloadSpeed;

        // 선택한 항목에 따라 다른 값을 얻어온다.
        switch (item)
        {
            case LevelItem.Armor:
                currLevel = St_LevelManager.Inst.armorLevel;
                maxLevel = St_LevelManager.Inst.armorLevelLimit;
                break;

            case LevelItem.Pellet:
                currLevel = St_LevelManager.Inst.pelletLevel;
                maxLevel = St_LevelManager.Inst.pelletLevelLimit;
                break;

            case LevelItem.ShootSpeed:
                currLevel = St_LevelManager.Inst.shootSpeedLevel;
                maxLevel = St_LevelManager.Inst.shootSpeedLevelLimit;
                break;

            case LevelItem.ReloadSpeed:
                currLevel = St_LevelManager.Inst.reloadSpeedLevel;
                maxLevel = St_LevelManager.Inst.reloadSpeedLevelLimit;
                break;
        }

        text.text = textContent + $"\n\n레벨 {currLevel}/{maxLevel}";
    }
}
