using UnityEngine;

public class St_PauseUI : MonoBehaviour
{
    public static St_PauseUI Inst;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        gameObject.SetActive(false); // 비활성화 상태로 시작
        Inst = this;
    }

    void OnDestroy()
    {
        Inst = null;
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
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if(St_UpdateManager.Inst.IsRunning()) // 업데이트가 활성화 되면 키 입력을 받지 않는다.
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) // esc를 누르면 일시정지 화면 비활성화
        { 
            Disable();
        }
    }
}
