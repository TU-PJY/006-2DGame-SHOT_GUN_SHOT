using UnityEngine;

using T = MatrixTransform;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 Offset;
    public float rotationLerpSpeed;
    public float zoomLerpSpeed;
    public float zoomOutOffset;

    private PlayerController playerInfo;

    private Camera cam;
    private Matrix4x4 camMatrix;
    private float camRot;
    private float camZoom = 1f;
    private float defaultCamZoom;

    void Awake()
    {
        cam = GetComponent<Camera>();
        playerInfo = targetObject.GetComponent<PlayerController>();
    }

    void Start()
    {
        // 시작 시 카메라가 초기 상태와 완전히 일치하도록 설정
        defaultCamZoom = cam.orthographicSize;
        camZoom = defaultCamZoom;
        camRot = targetObject.transform.rotation.eulerAngles.z - 90f;
    }

    void LateUpdate()
    {
        if(!targetObject) return;

        var targetPos = (Vector2)targetObject.transform.position;
        var targetRot = targetObject.transform.rotation.eulerAngles.z - 90f;

        // Time.deltaTime * rotationLerpSpeed 속도로 회전 각도 선형 변환
        camRot = Mathf.LerpAngle(camRot, targetRot, Time.deltaTime * rotationLerpSpeed);

        T.Identity(ref camMatrix);
        T.Translate2D(ref camMatrix, targetPos);
        T.Rotate2D(ref camMatrix, camRot);
        T.Translate2D(ref camMatrix, Offset);
        T.Dispatch(transform, ref camMatrix);

        // 달리는 동안에는 카메라 줌 아웃 // 달리지 않으면 줌 인
        camZoom = Mathf.Lerp(camZoom, playerInfo.runFlag ? defaultCamZoom + zoomOutOffset : defaultCamZoom, Time.deltaTime * zoomLerpSpeed);
        cam.orthographicSize = camZoom;
    }
}
