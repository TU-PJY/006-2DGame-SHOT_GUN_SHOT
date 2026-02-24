using UnityEngine;

public class MuzzleFire : MonoBehaviour
{
    public ObjectManager MyPool { set; private get; }

    private Animator anim;

    public void SetAnimSpeed(float speed)
    {
        if(!anim)
            anim = GetComponent<Animator>();
        anim.speed = speed;
    }

    public void DestroyMuzzleFire()
    {
        MyPool.ReturnMuzzleFire(this);
    }
}
