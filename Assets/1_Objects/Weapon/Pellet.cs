using System.Collections.Generic;
using UnityEngine;

struct RayStartEnd
{
    public RayStartEnd(Vector2 start_, Vector2 end_)
    {
        start = start_;
        end = end_;
    }

    public Vector2 start;
    public Vector2 end;
}

// 여러 개의 펠릿을 여러개의 Ray로 처리하여 몬스터 피격 처리
public class Pellet : MonoBehaviour
{
    [HideInInspector]
    public int pelletCount; // 펠릿 개수 (ray 개수) // 오브젝트 풀에서 생성 시 개수 입력 후 활성화

    [HideInInspector]
    public float pelletDisperse; // 펠릿이 퍼지는 각도

    [HideInInspector]
    public float pelletDistance; // 펠릿이 대미지를 줄수 있는 거리

    public ObjectManager MyPool { private get; set; }

    private float lineOpacity = 1f; // 선 불투명도

    private bool rayCasted = false; // 레이캐스트 실행 여부

    private LineRenderer line; // 펠릿 궤적을 표시하기 위한 라인 렌더러

    private List<RayStartEnd> rayPosList = new();

    public void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
    }

    public void ResetState()
    {
        lineOpacity = 1f;
        line.positionCount = pelletCount * 2;
        rayPosList.Clear();
        rayCasted = false;
    }

    void Update()
    {
        // 레이 캐스팅을 1회 실행한 후 레이 캐스팅 결과를 리스트에 담아 한꺼번에 선으로 렌더링
        if (!rayCasted)
        {
            var rayPos = (Vector2)transform.position;
            var rayRot = transform.rotation.eulerAngles.z;

            for (int i = 0; i < pelletCount; i++)
            {
                float radian = (rayRot + Random.Range(-pelletDisperse, pelletDisperse)) * Mathf.Deg2Rad;
                Vector2 direction = Math_.AngleDirection(radian);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, direction, pelletDistance);
                RayStartEnd rayStartEnd = new(rayPos, Math_.OffsetPosition(rayPos, direction, pelletDistance));

                // 장애물이나 몬스터와 충돌하면 다음 펠릿 처리
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        if (hit.collider.TryGetComponent<Monster>(out var monster))
                        {
                            monster.GiveDamage(10);
                            rayStartEnd.end = hit.point;
                        }
                    }
                }

                rayPosList.Add(rayStartEnd);
            }

            rayCasted = true;
        }

        else
        {
            // 선을 점차 투명하게 한다
            lineOpacity -= Time.deltaTime * 5f;
            line.startColor = new Color(1f, 1f, 1f, lineOpacity * 0.25f);
            line.endColor = new Color(1f, 1f, 1f, lineOpacity * 0.25f);

            // 생성한 레이들을 모두 선으로 렌더링
            for(int i = 0; i < pelletCount; i ++)
            {
                line.SetPosition(i * 2, rayPosList[i].start);
                line.SetPosition(i * 2 + 1, rayPosList[i].end);
            }

            // 선이 완전히 투명해지면 다시 풀로 반환
            if(lineOpacity <= 0f)
                MyPool.ReturnPellet(this);
        }
    }
}
