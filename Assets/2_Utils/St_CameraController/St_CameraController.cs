using UnityEngine;

using T = MatrixTransform;

public class St_CameraController : MonoBehaviour
{
    public static St_CameraController Inst;

    public Vector2 Offset;
    public float rotationLerpSpeed;
    public float zoomLerpSpeed;
    public float zoomOutOffset;
    public float shakeReduction;

    private Camera cam;
    private Matrix4x4 camMatrix;
    private float camRot;
    private float camZoom = 1f;
    private float defaultCamZoom;

    private Vector2 destPosition = new();
    private float destRotation = 0f;
    private bool runningState = false;

    private float currentShakeIntervalTime;
    private float currentShake;
    private Vector2 shakeOffset;

    void Awake()
    {
        if (Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }
        
        Inst = this;
        cam = Camera.main;
    }

    void Start()
    {
        // 시작 시 카메라가 초기 상태와 완전히 일치하도록 설정
        defaultCamZoom = cam.orthographicSize;
        camZoom        = defaultCamZoom;
        camRot         = destRotation - 90f;

        print("[CameraController] Created instance.");
    }

    public void InputPlayerPos(Vector2 position)
    {
        destPosition = position;
    }

    public void InputPlayerRotation(float degrees)
    {
        destRotation = degrees;
    }

    public void InputRunState(bool flag)
    {
        runningState = flag;
    }

    public void AddShake(float val)
    {
        currentShake += val;
    }

    void LateUpdate()
    {
        if(!St_UpdateManager.Inst.Check()) 
            return;
            
        UpdateShake();

        var targetPos = destPosition;
        var targetRot = destRotation - 90f;

        // Time.deltaTime * rotationLerpSpeed 속도로 회전 각도 선형 변환
        camRot = Mathf.LerpAngle(camRot, targetRot, Time.deltaTime * rotationLerpSpeed);
        //camRot = targetRot;

        T.Identity(ref camMatrix);
        T.Translate(ref camMatrix, targetPos);
        T.Rotate(ref camMatrix, camRot);
        T.Translate(ref camMatrix, Offset + shakeOffset);
        T.Dispatch(cam.transform, ref camMatrix);

        // 달리는 동안에는 카메라 줌 아웃 // 달리지 않으면 줌 인
        camZoom = Mathf.Lerp(camZoom, runningState ? defaultCamZoom + zoomOutOffset : defaultCamZoom, Time.deltaTime * zoomLerpSpeed);
        cam.orthographicSize = camZoom;
    }

    void UpdateShake()
    {
        currentShake = Mathf.Lerp(currentShake, 0f, Time.deltaTime * shakeReduction);
        currentShakeIntervalTime += Time.deltaTime;
        if (currentShakeIntervalTime >= 0.01f)
        {
            shakeOffset.x = Random.Range(-currentShake, currentShake);
            shakeOffset.y = Random.Range(-currentShake, currentShake);
            currentShakeIntervalTime -= 0.01f;
        }
    }

    public float GetRotation()
    {
        return camRot;
    }

    public void Release()
    {
        print("[CameraController] Released instance.");
        Inst = null;
    }
}
