using UnityEngine;

public class HealthItem : Item
{
    public override void ReturnInstance()
    {
        St_ObjectManager.Inst.ReturnHealthItem(this);
    }
}
