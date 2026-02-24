using UnityEngine;

public class MuzzleFire : MonoBehaviour
{
    private Animator anim;

    public void SetAnimSpeed(float speed)
    {
        if(!anim)
            anim = GetComponent<Animator>();
        anim.speed = speed;
    }

    public void DestroyMuzzleFire()
    {
        ObjectManager.Inst.ReturnMuzzleFire(this);
    }
}
