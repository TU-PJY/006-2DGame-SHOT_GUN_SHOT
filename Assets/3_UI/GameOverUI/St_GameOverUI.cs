using UnityEngine;

public class St_GameOverUI : MonoBehaviour
{
    public static St_GameOverUI Inst;
    public ResultText rText;

    void Awake()
    {
        if(Inst && Inst != this) {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        gameObject.SetActive(false); // 비활성화 상태로 시작
        print("[GameOverUI] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    // 게임 오버 시 호출
    // 인게임 UI가 비활성화 되고 게임오버 UI가 활성화 된다.
    // 업데이트가 정지된다.
    // 마우스 잠금이 해제된다.
    public void Enable()
    {
        St_InGameUI.Inst.Disable();
        St_UpdateManager.Inst.Pause();     
        St_MouseManager.Inst.UnlockCursor();
        gameObject.SetActive(true);
        rText.UpdateText();
    }
}
