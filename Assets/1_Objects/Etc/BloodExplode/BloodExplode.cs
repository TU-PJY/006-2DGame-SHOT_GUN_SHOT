using UnityEngine;

public class BloodExplode : MonoBehaviour
{
    private Vector2 originScale;
    private Animator anim;

    void Awake()
    {
        originScale = transform.localScale;
        anim = GetComponent<Animator>();
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

    public void ResetState()
    {
        transform.localScale = originScale;
    }

    public void OnDestroyBlood()
    {
        St_ObjectManager.Inst.ReturnBloodExplode(this);
    }
}
