using UnityEngine;

public class StartButton : MonoBehaviour
{
    public ScreenSwitcher sceneSwither;

    // PlayScene으로 이동한다
    public void OnButtonClick()
    {
        St_SoundPlayer.Inst.PlayButtonSound();
        var switcher = Instantiate(sceneSwither);
        switcher.sceneName = "PlayScene";
        gameObject.SetActive(false);
    }
}
