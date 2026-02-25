using UnityEngine;
using UnityEngine.UI;

public class St_RoundIndicator : MonoBehaviour
{
    public static St_RoundIndicator Inst;

    private Text text;
    private float textScale; // 피드백 재생 변수

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
        textScale = 1.5f;
    }
}
