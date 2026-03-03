using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public PauseUI pauseScreen;

    // 일시정지 화면을 비활성화 하고 업데이트를 재개한다
    public void OnButtonClick()
    {
        St_UpdateManager.Inst.Resume();
        pauseScreen.Disable();
    }
}
