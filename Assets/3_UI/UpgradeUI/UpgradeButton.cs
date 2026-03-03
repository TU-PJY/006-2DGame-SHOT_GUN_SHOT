using UnityEngine;

public class UpgradeButton : UpgradeItem
{
    public UpgradeUI upgradeScreen;

    public void OnClick()
    {
        if(usingArmorLevel)
            St_LevelManager.Inst.IncreaseArmorLevel();
        else if(usingPelletLevel)
            St_LevelManager.Inst.IncreasePelletLevel();
        else if(usingShootSpeedLevel)
            St_LevelManager.Inst.IncreaseShootSpeedLevel();
        else if(usingReloadSpeedLevel)
            St_LevelManager.Inst.IncreaseReloadSpeedLevel();
        else if(usingNearAttackDmgLevel)
            St_LevelManager.Inst.IncreaseNearAttackDmgLevel();

        // 다음 라운드 준비
        St_GameManager.Inst.SetNextRound();
        upgradeScreen.Disable();
    }
}
