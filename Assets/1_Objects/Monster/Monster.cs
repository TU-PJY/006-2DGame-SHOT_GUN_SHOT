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

    // 추적 대상 플레이어
    protected GameObject targetPlayer;

    protected virtual void Awake()
    {
        rigidBody.linearDamping = linearDamping;
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

    public virtual void GiveDamage(int damage)
    {

    }

    // 몬스터 사망 시 BloodExplode 객체를 생성 후 오브젝트 풀로 반환된다.
    protected virtual void DeleteInstance()
    {
        var newBloodExplosion = ObjectManager.Inst.GetBloodExplode();
        newBloodExplosion.transform.position = transform.position;
        newBloodExplosion.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180, 180));
        ObjectManager.Inst.ReturnMonster(this);
    }
}
