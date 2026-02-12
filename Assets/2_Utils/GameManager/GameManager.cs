using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    public float mouseSensivity;

    private List<Monster> monsterList = new();

    void Awake()
    {
        if(Inst && Inst == this)
        {
            print("[GameManager] Instance is exist already. Destroy duplicated instance.");
            DestroyImmediate(this);
            return;
        }

        print("[GameManager] Created new instance.");
        Inst = this;
        DontDestroyOnLoad(this);
    }
}
