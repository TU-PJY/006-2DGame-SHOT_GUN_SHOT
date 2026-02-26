using UnityEngine.UI;
using UnityEngine;
using System;
using System.Xml.Serialization;

public class St_BulletCountIndicator : MonoBehaviour
{
    public static St_BulletCountIndicator Inst;
    private Text text;
    public Image img;
    private float textScale; // 피드백 애니메이션 변수
    private bool started = false; // 게임 시작 때 첫 값 입력 시 피드백을 재생하지 않도록 한다

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        text = GetComponent<Text>();
        text.text = "0";
        textScale = 1f;

        print("[BulletIndicator] Created instance.");
    }

    void Update()
    {
        // 실시간 피드백 애니메이션 재생
        textScale -= Time.deltaTime * 5f;
        textScale = Mathf.Clamp(textScale, 1f, 1.5f);

        transform.localScale = new Vector2(textScale, textScale);
    }

    // 현재 탄약 개수를 텍스트 렌더러에 반영
    public void InputBulletCount(int val)
    {
        // 탄약 개수가 0일 경우 R을 표시하고 텍스트 색상을 빨강으로 변경한다.
        // 그렇지 않으면 다시 숫자를 표시하고 텍스트 색상을 흰색으로 변경한다.
        bool isZero = val == 0;
        text.text = isZero ? "R" : val.ToString();
        text.color = isZero ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 1f);
        img.color = isZero ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 1f);

        // 장탄수 입력이 감지되면 피드백 변수 업데이트
        if(started)
            textScale = 1.5f;
        started = true;
    }

    public void Release()
    {
        print("[BulletIndicator] Released instance.");
        Inst = null;
    }
}
