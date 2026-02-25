using UnityEngine;

public class BloodExplode : MonoBehaviour
{
    public void OnDestroyBlood()
    {
        St_ObjectManager.Inst.ReturnBloodExplode(this);
    }
}
