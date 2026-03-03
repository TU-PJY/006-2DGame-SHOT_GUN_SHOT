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

    void Update()
    {
        if(!St_UpdateManager.Inst.IsRunning())
        {
            anim.speed = 0f;
            return;
        }
        
        anim.speed = 1f;
    }

    public void DestroyMuzzleFire()
    {
        St_ObjectManager.Inst.ReturnMuzzleFire(this);
    }
}
