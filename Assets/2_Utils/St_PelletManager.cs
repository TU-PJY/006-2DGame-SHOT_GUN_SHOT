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
public class St_PelletManager : MonoBehaviour
{
    public static St_PelletManager Inst;

    private int iteration; // 펠릿 개수 (ray 개수) // 오브젝트 풀에서 생성 시 개수 입력 후 활성화
    private float disperse; // 펠릿이 퍼지는 각도
    private float distance; // 펠릿이 대미지를 줄수 있는 거리
    private float damage; // 펠릿 당 대미지
    private List<RayStartEnd> rayPosList = new();
    private Vector2 rayPos;
    private float rayRot;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        
        print("[PelletManager] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    // 레이 캐스팅을 시작하고 모든 상태를 리셋한다.
    public void RayCast(int iteration_, float disperse_, float distance_, float damage_, float knockBackForce)
    {
        rayPosList.Clear();
        rayPos = (Vector2)transform.position;
        rayRot = transform.rotation.eulerAngles.z;
        iteration = iteration_;
        disperse = disperse_;
        distance = distance_;
        damage = damage_;

        // 레이 캐스팅을 1회 실행한 후 레이 캐스팅 결과를 별도의 PelletRenderer에 전달하여 렌더링 하도록 한다
        ProcessRayCast(knockBackForce);
        foreach(var p in rayPosList)
        {
            var newPelletRenderer = St_ObjectManager.Inst.GetPelletRenderer();
            newPelletRenderer.SetPosition(p.start, p.end);
        }
    }

    private void ProcessRayCast(float force)
    {
        for (int i = 0; i < iteration; i++)
        {
            float radians = Math_.Radians(rayRot + Random.Range(-disperse, disperse));
            Vector2 direction = Math_.AngleDirection(radians);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, direction, distance);
            RayStartEnd rayStartEnd = new(Math_.OffsetPosition(rayPos, direction, 1f), Math_.OffsetPosition(rayPos, direction, distance));

            // 장애물이나 몬스터와 충돌하면 다음 펠릿 처리
            if (hit.collider != null)
            {
                var colliderTag = hit.collider.tag;

                switch(colliderTag)
                {
                    case "Enemy":
                        if (hit.collider.TryGetComponent<Monster>(out var monster))
                        {
                            monster.GiveDamage(damage);

                            // 충돌한 몬스터에 넉백 가함
                            Vector2 rayNormal = hit.normal;
                            Vector2 rayDirNormalized = rayNormal.normalized;
                            monster.GiveKnockback(force, -rayDirNormalized);

                            rayStartEnd.end = hit.point;

                            // 몬스터와 충돌한 곳에 피격 인디케이터 객체와 피 객체 생성
                            var newHitInd = St_ObjectManager.Inst.GetHitIndicator();
                            newHitInd.transform.position = hit.point;
                            newHitInd.ResetState();

                            var newBloodStain = St_ObjectManager.Inst.GetBloodStain();
                            newBloodStain.transform.position = hit.point;
                            newBloodStain.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));

                            newBloodStain.ResetState();
                            var monsterScale = monster.transform.localScale;
                            var bloodStainScale = newBloodStain.transform.localScale;
                            newBloodStain.transform.localScale = new Vector2(monsterScale.x * bloodStainScale.x, monsterScale.y * bloodStainScale.y);
                        }
                        break;

                    default:
                        rayStartEnd.end = hit.point;
                        break;
                }
            }

            rayPosList.Add(rayStartEnd);
        }
    }
}
