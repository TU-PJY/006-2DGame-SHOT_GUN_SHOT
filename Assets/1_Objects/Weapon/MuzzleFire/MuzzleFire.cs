using UnityEngine;

public class MuzzleFire : MonoBehaviour
{
    public ObjectPool MyPool { set; private get; }

    public void DestroyMuzzleFire()
    {
        MyPool.ReturnMuzzleFire(this);
    }
}
