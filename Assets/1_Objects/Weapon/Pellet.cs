using System.Collections.Generic;
using UnityEngine;

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

    private float deleteDelay = 0f;

    private bool rayCasted = false; // 레이캐스트 실행 여부

    private List<Ray> rayList = new();

    public void ResetState()
    {
        rayCasted = false;
    }

    void Update()
    {
        var rayPos = (Vector2)transform.position;
        var rayRot = transform.rotation.eulerAngles.z;

        if (!rayCasted)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                float radian = (rayRot + Random.Range(-pelletDisperse, pelletDisperse)) * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                RaycastHit2D hit = Physics2D.Raycast(rayPos, direction, pelletDistance);

                // 장애물이나 몬스터와 충돌하면 다음 펠릿 처리
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        if (hit.collider.TryGetComponent<Monster>(out var monster))
                        {
                            monster.GiveDamage(10);
                            Debug.DrawRay(rayPos, direction * hit.distance, Color.red, 0.5f);
                        }
                    }
                }

                else
                    Debug.DrawRay(rayPos, direction * pelletDistance, Color.red, 0.5f);
            }

            rayCasted = true;
        }
        else
        {
            deleteDelay -= Time.deltaTime;
            if(deleteDelay <= 0f)
            {
                deleteDelay = 0f;
                MyPool.ReturnPellet(this);
            }
        }
    }
}
