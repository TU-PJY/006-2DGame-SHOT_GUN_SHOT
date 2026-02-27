using System.Collections.Generic;
using UnityEngine;

public class NearAttackBound : MonoBehaviour
{
    public CapsuleCollider2D bound;

    private List<Collider2D> collideList = new();
    private ContactFilter2D filter;

    void Awake()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        filter.useLayerMask = true;
    }

    // Player에서 근접 공격을 활성화 하면 범위 내에 있는 몬스터에게 대미지를 준다.
    public void ProcessNearAttack(float damage)
    {
        print("Near attack");
        
        collideList.Clear();
        var count = bound.Overlap(filter, collideList);
        
        if(count == 0)
            return;

        foreach(var c in collideList)
        {
            var monster = c.GetComponent<Monster>();

            monster.GiveDamage(damage);

            var newBloodStain = St_ObjectManager.Inst.GetBloodStain();
            newBloodStain.transform.position = monster.transform.position;
            newBloodStain.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));

            newBloodStain.ResetState();
            var monsterScale = monster.transform.localScale;
            var bloodStainScale = newBloodStain.transform.localScale;
            newBloodStain.transform.localScale = new Vector2(monsterScale.x * bloodStainScale.x, monsterScale.y * bloodStainScale.y);

            St_CameraController.Inst.AddShake(0.25f);
        }
    }
}
