using UnityEngine;

public class MuzzleFire : MonoBehaviour
{
    public ObjectManager MyPool { set; private get; }

    public void DestroyMuzzleFire()
    {
        MyPool.ReturnMuzzleFire(this);
    }
}
