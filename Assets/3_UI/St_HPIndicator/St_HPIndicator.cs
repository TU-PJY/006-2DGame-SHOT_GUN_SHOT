using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class St_HPIndicator : MonoBehaviour
{
    public static St_HPIndicator Inst;

    public Image img;

    private Text text;
    private float colorVal = 1f; // 피격 피드백용 색상 값
    private Color textColor = new(1f, 1f, 1f, 1f);
    private bool started = false; // 게임 시작 때 첫 값 입력 시 피드백을 재생하지 않도록 한다

    void Awake()
    {
        if(Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }

        text = GetComponent<Text>();
        Inst = this;
        print("[HPIndicator] Created instance.");
    }

    void Update()
    {
        // 피격 피드백 재생 // 순간적으로 빨간색이 되었다가 천천히 흰색으로 되돌아간다
        colorVal += Time.deltaTime * 2f;
        colorVal = Mathf.Clamp(colorVal, 0f, 1f);
        textColor.g = colorVal;
        textColor.b = colorVal;
        text.color = textColor;
        img.color = text.color;
    }

    public void InputHP(int val)
    {
        text.text = val.ToString();
        // 피해를 입으면 색상을 빨간색으로 변경한다
        if(started)
            colorVal = 0f;
        started = true;
    }

    public void Release()
    {
        print("[HPIndicator] Released instance.");
        Inst = null;
    }
}
