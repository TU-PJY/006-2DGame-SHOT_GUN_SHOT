using UnityEngine;
using UnityEngine.UIElements;

public class St_ReloadIndicator : MonoBehaviour
{
    public static St_ReloadIndicator Inst;
    private Vector2 originScale;
    private RectTransform rTransform;

    void Awake()
    {
        if(Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;

        rTransform = GetComponent<RectTransform>();

        // 초기 스케일값을 필드에 저장
        originScale = rTransform.localScale;

        // 재장전 전이므로 비활성화 상태로 설정
        gameObject.SetActive(false);

        print("[ReloadIndicator] Created instance.");
    }

    // 재장전 시각화가 보이지 않도록 설정
    public void SetInvisible()
    {
        gameObject.SetActive(false);
    }

    // 설정 재장전 소요 시간과 현재 재장전 누적 시간을 입력하여 재장전 딜레이 시각화
    // 이 함수를 호출하면 렌더링이 다시 활성화됨
    public void InputReloadTime(float totalTime, float currentTime)
    {
        gameObject.SetActive(true);

        // 현재 재장전 누적 시간이 설정 소요 시간에 가까울수록 참
        rTransform.localScale = new Vector2((totalTime - currentTime) / totalTime * originScale.x, originScale.y);
    }

    public void Release()
    {
        print("[ReloadIndicator] Released instance.");
        Inst = null;
    }
}
