using UnityEngine;

public class UpgradeButton : UpgradeItem
{
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

        // 다음 라운드 준비
        St_GameManager.Inst.SetNextRound();
    }
}
