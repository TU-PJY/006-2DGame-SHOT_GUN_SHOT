using UnityEngine;
using UnityEngine.UI;

public class St_DamageIndicator : MonoBehaviour
{
    public static St_DamageIndicator Inst;
    public Image img;

    private Color imageColor;
    private float opacity = 0f;
    private bool working = false;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        imageColor = img.color;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f); // 투명화 상태로 시작
        Inst = this;

        print("[DamageIndicator] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    void Update()
    {
        if (!working)
            return;

        Color newColor = imageColor;
        opacity -= Time.deltaTime * 2f;
        opacity = Mathf.Clamp(opacity, 0f, 1f);
        newColor.a = opacity;
        img.color = newColor;

        // 완전히 투명해지면 업데이트 중단
        if (newColor.a <= 0f)
            working = false;
    }

    // 대미지를 받을 때마다 활성화
    public void Enable()
    {
        opacity = 1f;
        working = true;
    }
}
