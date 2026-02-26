using UnityEngine;

public static class GlobalMethod
{
    // static instance를 사용하는 모듈들을 할당 해제한다.
    public static void ReleaseStaticModules()
    {
        St_BulletCountIndicator.Inst.Release();
        St_CameraController.Inst.Release();
        St_GameManager.Inst.Release();
        St_HPIndicator.Inst.Release();
        St_InGameUI.Inst.Release();
        St_LevelManager.Inst.Release();
        St_MonsterGenerator.Inst.Release();
        St_MouseManager.Inst.Release();
        St_ObjectManager.Inst.Release();
        St_PelletManager.Inst.Release();
        St_ReloadIndicator.Inst.Release();
        St_RemainEnemyIndicator.Inst.Release();
        St_RoundIndicator.Inst.Release();
        St_UpdateManager.Inst.Release();
        St_UpgradeUI.Inst.Release();
    }
}
