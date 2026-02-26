using UnityEngine;

public class St_MouseManager : MonoBehaviour
{
    public static St_MouseManager Inst;

    public float sensivity;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }    

        Inst = this;
        print("[MouseManager] Created instance.");
    }

    public void Release()
    {
        print("[MouseManager] Released instance.");
        Inst = null;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
