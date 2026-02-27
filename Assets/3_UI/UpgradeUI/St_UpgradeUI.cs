using UnityEngine;
using UnityEngine.UI;

public class St_UpgradeUI : MonoBehaviour
{
    public static St_UpgradeUI Inst;

    public Button[] buttons;
    public bool enableAtStart;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[UpgradeUI] Created instance.");

        // 개발용 변수
        if(!enableAtStart)
            gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Inst = null;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        foreach(var b in buttons)
            b.GetComponentInChildren<UpgradeText>().UpdateText();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
