using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // 게임 종료
    public void OnButtonClick()
    {
        print("[ExitButton] Exit button clicked.");
        St_SoundPlayer.Inst.PlayButtonSound();
        Application.Quit();
    }
}
