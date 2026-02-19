using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Monster monsterPrefab;
    public MuzzleFire muzzleFirePrefab;
    
    private List<Monster> monsterPool = new();
    private List<MuzzleFire> muzzleFirePool = new();

    public T GetInstance<T> (ref List<T> pool, T preFab) where T : MonoBehaviour
    {
        while (pool.Count > 0)
        {
            var inst = pool[0];
            pool.RemoveAt(0);

            if (inst != null)
            {
                inst.gameObject.SetActive(true);
                return inst;
            }
        }

        var newInst = Instantiate(preFab);
        return newInst;
    }

    public void ReturnInstance<T> (ref List<T> pool, T instance) where T : MonoBehaviour
    {
        if (pool.Contains(instance))
            return;
        instance.gameObject.SetActive(false);
       // instance.transform.SetParent(transform);
        pool.Add(instance);
    }

    public Monster GetMonster()
    {
        var newInst = GetInstance(ref monsterPool, monsterPrefab);
        newInst.MyPool = this;
        return newInst;
    }

    public MuzzleFire GetMuzzleFire()
    {
        var newInst = GetInstance(ref muzzleFirePool, muzzleFirePrefab);
        newInst.MyPool = this;
        return newInst;
    }

    public void ReturnMonster(Monster m)
    {
        ReturnInstance(ref monsterPool, m);
    } 

    public void ReturnMuzzleFire(MuzzleFire m)
    {
        ReturnInstance(ref muzzleFirePool, m);
    }
}
