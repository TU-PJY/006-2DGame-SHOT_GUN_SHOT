using UnityEngine;

public class AmmoItem : Item
{
    public override void ReturnInstance()
    {
        St_ObjectManager.Inst.ReturnAmmoItem(this);
    }
}
