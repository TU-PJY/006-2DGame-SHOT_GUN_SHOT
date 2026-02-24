using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Inst;

    public Monster monsterPrefab;
    public MuzzleFire muzzleFirePrefab;
    public Pellet pelletPrefab;
    
    private List<Monster> monsterPool = new();
    private List<MuzzleFire> muzzleFirePool = new();
    private List<Pellet> pelletPool = new();

    void Awake()
    {
        if(Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[ObjectManager] Created instance.");
    }

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

    public Pellet GetPellet()
    {
        var newInst = GetInstance(ref pelletPool, pelletPrefab);
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

    public void ReturnPellet(Pellet p)
    {
        ReturnInstance(ref pelletPool, p);
    }
}
