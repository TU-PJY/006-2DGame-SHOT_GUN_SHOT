using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public Monster monsterPrefab;
    private List<Monster> pool = new();

    public Monster GetMonster()
    {
        if (pool.Count == 0)
        {
            var inst = Instantiate(monsterPrefab);
            inst.MyPool = this;
            return inst;
        }
        else
        {
            var inst = pool[0];
            pool.RemoveAt(0); 
            inst.gameObject.SetActive(true);
            inst.transform.SetParent(null);
            return inst;
        }
    }

    public void ReturnMonster(Monster m)
    {
        if (pool.Contains(m))
            return;
        m.gameObject.SetActive(false); 
        m.transform.SetParent(transform);
        pool.Add(m);
    }
}
