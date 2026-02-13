using UnityEngine;

using T = MatrixTransform;

public class Zombie : Monster
{
    private bool isAttack = false;
    private bool isWalk = false;
    private bool isNear = false;
    private float rotation;
    private float rotationDest;

    protected override void Awake()
    {
        // 바디의 선형 댐핑 설정
        base.Awake();

        // 초기 회전 값 랜덤 설정
        rotation = Random.Range(0f, 180f);
    }

    protected override void Start()
    {
        // 플레이어 탐색
        base.Start();
    }

    protected override void Update()
    {
        CheckPlayerNear();
        MoveBody();
        RotateBody();
        AttackPlayer();
    }

    protected override void FixedUpdate()
    {
        TrackPlayer();
    }

    // 플레이어 리지드 바디 - 트리거 경계 충돌 시 공격 상태 판정 
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isAttack = true;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isAttack = false;
    }

    void CheckPlayerNear()
    {
         // 플레이가 visonRange에 도달하면 추적 시작
         isNear = Mathv.CalcDistance(rigidBody.position, targetPlayer.transform.position) < visonRange;
    }

    void MoveBody()
    {
        // 공격 상태에서는 멈추도록 한다
        isWalk = isNear && !isAttack;
        anim.SetBool("IsWalking", isWalk);
        anim.speed = isWalk ? accSpeed / standardAccSpeed : 1f;
    }

    void RotateBody()
    {
        if (isNear)
            rotationDest = Mathv.CalcDegrees(rigidBody.position, targetPlayer.transform.position);
    }
    void AttackPlayer()
    {
        anim.SetBool("IsAttacking", isAttack);
    }

    void TrackPlayer()
    {
        // 회전 시 선형보간으로 부드럽게 회전
        rigidBody.rotation = Mathf.LerpAngle(rigidBody.rotation, rotationDest, 2f * Time.fixedDeltaTime);

        // 힘을 로컬 기준 앞으로만 가함
        // -90도 회전 오프셋이 있으므로 right 벡터 사용
        if(isWalk)
            rigidBody.AddForce(transform.right.normalized * accSpeed, ForceMode2D.Force);
    }
}
