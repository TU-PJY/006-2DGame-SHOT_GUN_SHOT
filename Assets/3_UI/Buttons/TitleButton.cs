using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public ScreenSwitcher sceneSwitcher;
    public LayoutGroup layout;

    // 타이틀로 돌아간다.
    public void OnButtonClick()
    {
        var newSwitcher = Instantiate(sceneSwitcher);
        newSwitcher.sceneName = "TitleScene";
        layout.gameObject.SetActive(false);
    }
}
