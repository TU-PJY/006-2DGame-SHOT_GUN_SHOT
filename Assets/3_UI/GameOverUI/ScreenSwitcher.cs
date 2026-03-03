using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 화면 전환 시 사용하는 모듈
public class ScreenSwitcher : MonoBehaviour
{
    public Image img;
    public float switchSpeed;

    [HideInInspector]
    public string sceneName;
    
    private float opacity = 0f;
    private bool sceneSwiched = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        img.color = new Color(0f, 0f, 0f, 0f);
    }

    void Update()
    {
        // 완전히 어두워지면 씬을 전환하고, 전환 이후 다시 완전히 투명해지면 스스로 삭제한다.
        if(!sceneSwiched)
        {
            opacity += Time.deltaTime * switchSpeed;
            if(opacity >= 1f)
            {
                opacity = 1f;
                SceneManager.LoadScene(sceneName);
                sceneSwiched = true;
            }
        }
        else
        {
            opacity -= Time.deltaTime * switchSpeed;
            if(opacity <= 0f)
            {
                Destroy(gameObject);
            }
        }
        
        img.color = new Color(0f, 0f, 0f, opacity);
    }
}
