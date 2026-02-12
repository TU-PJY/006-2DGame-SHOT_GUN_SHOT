using UnityEngine;
using UnityEngine.InputSystem;

enum moveDir
{
    Up,
    Down,
    Right,
    Left
}

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float linearDamping;
    public float accSpeed;
    public float runSpeedRatio;

    private bool[] moveFlag = new bool[]{false, false, false, false};
    private Vector2 force;
    private float rotation;

    // MainCamera->CameraController에서 참조
    [HideInInspector]
    public bool runFlag = false;

    void Awake()
    {
        rigidBody.linearDamping = linearDamping;
    }

    void Start()
    {
        // 마우스 고정 후 숨김
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        InputMouse();
        InputKey();
        UpdateAcc();
    }

    void FixedUpdate()
    {
        UpdateBody();
    }

    void InputKey() // 키 입력 
    {
        moveFlag[(int)moveDir.Up]    = Input.GetKey(KeyCode.W);
        moveFlag[(int)moveDir.Down]  = Input.GetKey(KeyCode.S);
        moveFlag[(int)moveDir.Right] = Input.GetKey(KeyCode.D);
        moveFlag[(int)moveDir.Left]  = Input.GetKey(KeyCode.A);

        // LSHIFT 입력 시 달리기
        runFlag = Input.GetKey(KeyCode.LeftShift);
    }

    void InputMouse()
    {
        // 마우스 회전 시 바디 회전
        rotation -= Input.mousePositionDelta.x * GameManager.Inst.mouseSensivity;

        // 0 ~ 360도 사이에서만 회전하도록 클램프
        if(rotation > 360f)    rotation = 0f;
        else if(rotation < 0f) rotation = 360f;
    }

    void UpdateAcc() // 가속도 업데이트
    {
        // 입력한 키의 방향에 해당하는 force를 1f 또는 -1f로 변경
        force = Vector2.zero;
        if (moveFlag[(int)moveDir.Up])    force.y += 1f;
        if (moveFlag[(int)moveDir.Down])  force.y -= 1f;
        if (moveFlag[(int)moveDir.Right]) force.x += 1f;
        if (moveFlag[(int)moveDir.Left])  force.x -= 1f;
    }

    void UpdateBody() // 실제 바디 움직임 업데이트
    {
        // rotation 만큼 회전
        rigidBody.SetRotation(rotation);

        // 달리기 상태에서는 relForce * accSpeed * runSpeedRatio 만큼 force 부여
        float runAccRatio = runFlag ? runSpeedRatio : 1f;

        // 회전 방향을 향하는 force
        Vector2 relForce = transform.right * force.y - transform.up * force.x;

        rigidBody.AddForce(relForce.normalized * accSpeed * runAccRatio, ForceMode2D.Force);     
    }
}
