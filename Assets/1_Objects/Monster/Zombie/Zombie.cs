using UnityEngine;

public class Zombie : Monster
{
    private bool isAttack = false;
    private bool isWalk = false;
    private bool isNear = false;
    private float rotationDest;
    private float totalDamage;

    public override void ResetState()
    {
        isAttack = false;
        isWalk = false;
        isNear = false;
        currAttackDamage = attackDamage;
        currHP = totalHP;
        weight = defaultWeight;
        totalDamage = 0f;
    }

    protected override void Awake()
    {
        // 바디의 선형 댐핑 설정
        base.Awake();
        currAttackDamage = attackDamage;
        currHP = totalHP;
    }

    protected override void Start()
    {
        // 플레이어 탐색
        base.Start();
    }

    protected override void Update()
    {
        if(!St_UpdateManager.Inst.IsRunning()) {
            anim.speed = 0f;
            return;
        }
            
        anim.speed = 1f;
        CheckPlayerNear();
        MoveBody();
        RotateBody();
        AttackPlayer();
        CalcHitCount();
    }

    protected override void FixedUpdate()
    {
        if(!St_UpdateManager.Inst.IsRunning())
            return;
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
        isNear = Math_.CalcDistance(rigidBody.position, targetPlayer.transform.position) < visonRange;
    }

    void MoveBody()
    {
        // 공격 상태에서는 멈추도록 한다
        isWalk = isNear && !isAttack;
        anim.SetBool("IsWalking", isWalk);
        if(isWalk)
            anim.speed = accSpeed / standardAccSpeed;
    }

    void RotateBody()
    {
        if (isNear)
            rotationDest = Math_.CalcDegrees(rigidBody.position, targetPlayer.transform.position);
    }

    void AttackPlayer()
    {
        anim.SetBool("IsAttacking", isAttack);
        if (isAttack)
            anim.speed = attackSpeed / standardAttackSpeed;
    }

    public void AnimEvent_OnAttack()
    {
        print("[Zombie] Attack event occured");
        targetPlayer.GetComponent<Player>().GiveDamage(currAttackDamage);
    }

    void TrackPlayer()
    {
        // 회전 시 선형보간으로 부드럽게 회전
        rigidBody.rotation = Mathf.LerpAngle(rigidBody.rotation, rotationDest, 5f * Time.fixedDeltaTime);

        // 힘을 로컬 기준 앞으로만 가함
        // -90도 회전 오프셋이 있으므로 right 벡터 사용
        if (isWalk)
            rigidBody.AddForce(transform.right.normalized * accSpeed, ForceMode2D.Force);
    }

    void CalcHitCount()
    {
        if (totalDamage > 0f)
        {
            currHP -= totalDamage;
            currHP = Mathf.Clamp(currHP, 0f, 9999f);
            if(currHP <= 0f)
                DeleteInstance();
            totalDamage = 0f;
        }
    }

    // 한 번에 대미지를 가하는 것이 아닌 대미지를 합산하여 나중에 처리한다.
    public override void GiveDamage(float damage)
    {
        totalDamage += damage;
    }

    // 총알에 맞으면 뒤로 밀린다
    // 무게가 무거우면 덜 밀린다
    public override void GiveKnockback(float force, Vector2 direction)
    {
        rigidBody.AddForce(force * direction / weight, ForceMode2D.Impulse);
        latestDirection = direction;
    }
}
