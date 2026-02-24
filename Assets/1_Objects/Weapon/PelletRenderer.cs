using UnityEngine;

public class PelletRenderer : MonoBehaviour
{
    private LineRenderer line; // 펠릿 궤적을 표시하기 위한 라인 렌더러

    private float opacity = 1f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
        line.positionCount = 2;
    }

    // 위치를 지정하고 불투명도를 리셋한다. 
    public void SetPosition(Vector2 start, Vector2 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        opacity = 1f;
    }

    void Update()
    {
        // 선을 점차 투명하게 한다
        opacity -= Time.deltaTime * 5f;
        line.startColor = new Color(1f, 1f, 1f, opacity * 0.25f);
        line.endColor = new Color(1f, 1f, 1f, opacity * 0.25f);

        // 선이 완전히 투명해지면 다시 풀로 반환
        if (opacity <= 0f)
            ObjectManager.Inst.ReturnPelletRenderer(this);
    }
}
