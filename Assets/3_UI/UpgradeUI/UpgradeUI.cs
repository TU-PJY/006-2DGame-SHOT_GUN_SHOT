using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button[] buttons;
    public bool enableAtStart;

    void Start()
    {
        Enable();
    }

    public void Enable()
    {
        St_InGameUI.Inst.Disable();
        St_UpdateManager.Inst.Pause();     
        St_MouseManager.Inst.UnlockCursor();
        
        foreach(var b in buttons)
            b.GetComponentInChildren<UpgradeText>().UpdateText();
    }

    public void Disable()
    {
        Destroy(gameObject);
    }
}
