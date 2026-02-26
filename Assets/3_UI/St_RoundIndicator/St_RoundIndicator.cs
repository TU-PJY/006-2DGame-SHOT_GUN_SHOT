using UnityEngine;
using UnityEngine.UI;

public class St_RoundIndicator : MonoBehaviour
{
    public static St_RoundIndicator Inst;

    private Text text;
    private float textScale; // 피드백 재생 변수
    private bool started = false; // 게임 시작 때 첫 값 입력 시 피드백을 재생하지 않도록 한다

    void Awake()
    {
        if(Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        text = GetComponent<Text>();
        print("[RoundIndicator] Created instance.");
    }

    void Update()
    {
        // 값에 변화가 발생하면 피드백 재생
        textScale -= Time.deltaTime * 5f;
        textScale = Mathf.Clamp(textScale, 1f, 1.5f);
        transform.localScale = new Vector2(textScale, textScale);
    }

    public void InputRound(int val)
    {
        text.text = $"Round {val}";
        if(started)
            textScale = 1.5f;
        started = true;
    }

    public void Release()
    {
        print("[RoundIndicator] Released instance.");
        Inst = null;
    }
}
