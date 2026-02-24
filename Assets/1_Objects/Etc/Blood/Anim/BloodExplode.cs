using UnityEngine;

public class BloodExplode : MonoBehaviour
{
    public void OnDestroyBlood()
    {
        ObjectManager.Inst.ReturnBloodExplode(this);
    }
}
