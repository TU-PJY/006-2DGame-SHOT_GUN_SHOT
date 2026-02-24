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
        opacity -= Time.deltaTime * 2f;
        sr.color = new Color(1f, 1f, 1f, opacity);

        var camRot = CameraController.Inst.GetRotation();
        transform.rotation = Quaternion.Euler(0f, 0f, camRot);

        if(opacity <= 0f)
            ObjectManager.Inst.ReturnHitIndicator(this);
    }
}
