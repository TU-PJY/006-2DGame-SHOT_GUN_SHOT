using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬을 다시 로드한다
public class RestartButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
