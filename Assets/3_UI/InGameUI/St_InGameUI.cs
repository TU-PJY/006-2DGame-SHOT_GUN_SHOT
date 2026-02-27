using UnityEngine;

public class St_InGameUI : MonoBehaviour
{
    public static St_InGameUI Inst;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Inst = this;
        print("[InGameUI] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
