using UnityEngine;
using UnityEngine.UI;

public class RoundStartUI : MonoBehaviour
{
    public Text text;
    private string originString;
    private float opacity = 0f;
    private float timer = 0f;
    bool disappearState = false;

    // 매 라운드 시작마다 라운드를 보여준다.
    void Start()
    {
        originString = text.text;
        var modified = originString;
        modified = modified.Replace("{}", St_GameManager.Inst.currentRound.ToString());
        text.text = modified;
        text.color = new Color(1f, 1f, 1f, 0f);

        St_SoundPlayer.Inst.PlayRoundStartSound();
    }

    // 잠시 나타났다가 사라진다.
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 2f)
            disappearState = true;

        if(!disappearState)
        {
            opacity += Time.deltaTime;
            opacity = Mathf.Clamp(opacity, 0f, 1f);
        }
        else
        {
            opacity -= Time.deltaTime;
            if(opacity <= 0f)
                Destroy(gameObject);
        }

        text.color = new Color(1f, 1f, 1f, opacity);
    }
}
