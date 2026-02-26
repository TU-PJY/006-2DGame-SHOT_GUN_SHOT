using UnityEngine;
using UnityEngine.UI;

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

        // 선택한 항목에 따라 다른 값을 얻어온다.
        if(usingArmorLevel)
        {
            currLevel = St_LevelManager.Inst.armorLevel;
            maxLevel = St_LevelManager.Inst.armorLevelLimit;
        }
        else if(usingPelletLevel)
        {
            currLevel = St_LevelManager.Inst.pelletLevel;
            maxLevel = St_LevelManager.Inst.pelletLevelLimit;
        }
        else if(usingShootSpeedLevel)
        {
            currLevel = St_LevelManager.Inst.shootSpeedLevel;
            maxLevel = St_LevelManager.Inst.shootSpeedLevelLimit;
        }
        else if(usingReloadSpeedLevel)
        {
            currLevel = St_LevelManager.Inst.reloadSpeedLevel;
            maxLevel = St_LevelManager.Inst.reloadSpeedLevelLimit;
        }

        text.text = textContent + $"\n\n레벨 {currLevel}/{maxLevel}";
    }
}
