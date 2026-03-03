using UnityEngine;

public class PauseUI : MonoBehaviour
{
    void Awake()
    {
        Enable();
    }

    // 업데이트를 일시정지하고 일시정지 화면을 활성화
    public void Enable()
    {
        St_InGameUI.Inst.Disable();
        St_MouseManager.Inst.UnlockCursor();
        St_UpdateManager.Inst.Pause();
        gameObject.SetActive(true);
    }

    // 업데이트를 재개하고 일시정지 화면을 비활성화
    public void Disable()
    {
        St_InGameUI.Inst.Enable();
        St_MouseManager.Inst.LockCursor();
        St_UpdateManager.Inst.Resume();
        Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc를 누르면 일시정지 화면 비활성화
        { 
            Disable();
        }
    }
}
