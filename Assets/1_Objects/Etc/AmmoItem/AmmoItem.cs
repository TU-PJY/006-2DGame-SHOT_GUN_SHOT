using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public void ReturnInstance()
    {
        St_ObjectManager.Inst.ReturnAmmoItem(this);
    }
}
