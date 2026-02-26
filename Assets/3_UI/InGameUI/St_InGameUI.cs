using UnityEngine;

public class St_InGameUI : MonoBehaviour
{
    public static St_InGameUI Inst;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Inst = this;
        print("[InGameUI] Created instance.");
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Release()
    {
        St_BulletCountIndicator.Inst.Release();
        St_HPIndicator.Inst.Release();
        St_ReloadIndicator.Inst.Release();
        St_RemainEnemyIndicator.Inst.Release();
        St_RoundIndicator.Inst.Release();
        Inst = null;
        print("[InGameUi] Released instance.");
    }
}
