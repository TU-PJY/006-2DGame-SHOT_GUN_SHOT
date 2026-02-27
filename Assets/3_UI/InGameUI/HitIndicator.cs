using System.Data.Common;
using UnityEngine;

public class HitIndicator : MonoBehaviour
{
    private SpriteRenderer sr;
    private float opacity = 1f;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ResetState()
    {
        opacity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!St_UpdateManager.Inst.IsRunning())
            return;
            
        opacity -= Time.deltaTime * 2f;
        sr.color = new Color(1f, 1f, 1f, opacity);

        var camRot = St_CameraController.Inst.GetRotation();
        transform.rotation = Quaternion.Euler(0f, 0f, camRot);

        if(opacity <= 0f)
            St_ObjectManager.Inst.ReturnHitIndicator(this);
    }
}
