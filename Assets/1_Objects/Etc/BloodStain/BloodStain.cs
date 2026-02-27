using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BloodStain : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[]{};
    float opacity;
    float deleteDelay;
    SpriteRenderer sr;
    
    public Vector2 originScale;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        opacity = 1f;
        deleteDelay = 1f;
        originScale = transform.localScale;
    }

    void Update()
    {
        if (!St_UpdateManager.Inst.Check())
            return;

        // 생성 1초 후 지워지기 시작하고, 완전히 지워지면 오브젝트 풀로 반환
        deleteDelay -= Time.deltaTime;
        if(deleteDelay <= 0f) {
            opacity -= Time.deltaTime * 0.25f;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, opacity);
            if(opacity <= 0f)
                St_ObjectManager.Inst.ReturnBloodStain(this);
        }
    }

    // 여러 스프라이트 중 하나를 랜덤 선택
    // 나머지 상태는 모두 리셋
    public void ResetState()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length - 1)];
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        opacity = 1f;
        deleteDelay = 1f;
        transform.localScale = originScale;
    }
}
