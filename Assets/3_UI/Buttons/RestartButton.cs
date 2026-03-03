using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 씬을 다시 로드한다
public class RestartButton : MonoBehaviour
{
    public ScreenSwitcher sceneSwitcher;
    public LayoutGroup layout;

    public void OnButtonClick()
    {
        var newSwitcher = Instantiate(sceneSwitcher);
        newSwitcher.sceneName = "PlayScene"; // 플레이 씬으로 전환(다시시작)
        layout.gameObject.SetActive(false); // ui 레이아웃을 비활성화 하며 의도치 않은 조작을 방지
    }
}
