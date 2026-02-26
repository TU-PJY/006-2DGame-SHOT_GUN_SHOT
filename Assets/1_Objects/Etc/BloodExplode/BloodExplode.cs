using UnityEngine;

public class BloodExplode : MonoBehaviour
{
    private Vector2 originScale;

    void Awake()
    {
        originScale = transform.localScale;
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
