using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Inst;

    public Monster monsterPrefab;
    public MuzzleFire muzzleFirePrefab;
    public PelletRenderer pelletRendererPrefab;
    public HitIndicator hitIndPrefab;
    public BloodStain bloodStainIndPrefab;
    public BloodExplode bloodExplodePrefab;
    
    private List<Monster> monsterPool = new();
    private List<MuzzleFire> muzzleFirePool = new();
    private List<PelletRenderer> pelletRendererPool = new();
    private List<HitIndicator> hitIndPool = new();
    private List<BloodStain> bloodStainPool = new();
    private List<BloodExplode> bloodExplodePool = new();

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
        return GetInstance(ref monsterPool, monsterPrefab);
    }

    public MuzzleFire GetMuzzleFire()
    {
        return GetInstance(ref muzzleFirePool, muzzleFirePrefab);
    }

    public PelletRenderer GetPelletRenderer()
    {
        return GetInstance(ref pelletRendererPool, pelletRendererPrefab);
    }

    public HitIndicator GetHitIndicator()
    {
        return GetInstance(ref hitIndPool, hitIndPrefab);
    }

    public BloodStain GetBloodStain()
    {
        return GetInstance(ref bloodStainPool, bloodStainIndPrefab);
    }

    public BloodExplode GetBloodExplode()
    {
        return GetInstance(ref bloodExplodePool, bloodExplodePrefab);
    }

    public void ReturnMonster(Monster m)
    {
        ReturnInstance(ref monsterPool, m);
    } 

    public void ReturnMuzzleFire(MuzzleFire m)
    {
        ReturnInstance(ref muzzleFirePool, m);
    }

    public void ReturnPelletRenderer(PelletRenderer p)
    {
        ReturnInstance(ref pelletRendererPool, p);
    }

    public void ReturnHitIndicator(HitIndicator h)
    {
        ReturnInstance(ref hitIndPool, h);
    }

    public void ReturnBloodStain(BloodStain b)
    {
        ReturnInstance(ref bloodStainPool, b);
    }

    public void ReturnBloodExplode(BloodExplode b)
    {
        ReturnInstance(ref bloodExplodePool, b);
    }
}
