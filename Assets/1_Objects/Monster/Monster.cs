using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator anim;

    public float visonRange;
    public float linearDamping;

    // 애니메이션 속도 변화량을 결정하는 기준 속도
    // 프레임 수를 사용한다.
    public float standardAccSpeed;
    public float accSpeed;

    // 애니메이션 속도 변화량을 결정하는 기준 속도
    // 프레임 수를 사용한다.
    public float standardAttackSpeed;
    public float attackSpeed;

    public float attackDamage;
    [HideInInspector]
    public float currAttackDamage;

    public float totalHP;
    [HideInInspector]
    public float currHP;

    public float defaultWeight;
    [HideInInspector]
    public float weight;

    [HideInInspector]
    public bool isBig;

    // 두 퍼센테이지의 합이 100을 넘어서는 안 된다.
    // 두 퍼센테이지의 합이 100 미만일 때 나머지 값은 아이템을 드랍하지 않을 확률이다.
    public int ammoDropPercentage;
    public int healthDropPercentage;

    protected Vector2 latestDirection;

    // 추적 대상 플레이어
    protected GameObject targetPlayer;

    protected virtual void Awake()
    {
        rigidBody.linearDamping = linearDamping;
        weight = defaultWeight;
    }

    protected virtual void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {

    }

    protected virtual void OnCollisionStay2D(Collision2D other)
    {

    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {

    }

    public virtual void ResetState()
    {
        
    }

    public virtual void GiveDamage(float damage)
    {

    }

    public virtual void GiveKnockback(float force, Vector2 direction)
    {
        
    }

    // 몬스터 사망 시 BloodExplode 객체를 생성 후 오브젝트 풀로 반환된다.
    // GameManager.Inst.remainedEnemy를 감소시킨다.
    // 2가지 아이템 중 랜덤으로 하나를 드랍한다.
    // 일정 확률로 드랍 여부가 결정된다.
    protected virtual void DeleteInstance()
    {
        var newBloodExplosion = St_ObjectManager.Inst.GetBloodExplode();
        newBloodExplosion.transform.position = transform.position;
        newBloodExplosion.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180, 180));
        newBloodExplosion.ResetState();
        var monsterScale = transform.localScale;
        var bloodExplodeScale = newBloodExplosion.transform.localScale;
        newBloodExplosion.transform.localScale = new Vector2(monsterScale.x * bloodExplodeScale.x, monsterScale.y * bloodExplodeScale.y);

        int randomVal = Range_.GenInt(1, 100);

        // 탄약 아이템 드랍
        if (Range_.InRange(randomVal, 1, ammoDropPercentage))
        {
            var newAmmoItem = St_ObjectManager.Inst.GetAmmoItem();
            newAmmoItem.SetState(transform.position, latestDirection);
            print("ammo item created");
        }

        // 회복 아이템 드랍
        else if(Range_.InRange(randomVal, ammoDropPercentage + 1, ammoDropPercentage + healthDropPercentage))
        {
            var newHealthItem = St_ObjectManager.Inst.GetHealthItem();
            newHealthItem.SetState(transform.position, latestDirection);
            print("health item created");
        }

        // 아무것도 드랍하지 않음

        St_GameManager.Inst.remainedEnemy--;
        St_RemainEnemyIndicator.Inst.InputRemainedEnemy(St_GameManager.Inst.remainedEnemy);
        St_ObjectManager.Inst.ReturnMonster(this);
    }
}
