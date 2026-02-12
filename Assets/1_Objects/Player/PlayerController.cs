using TMPro;
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
    public Animator anim;
    public Animator legAnim;
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
        UpdateAnim();
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

        // 왼쪽 shift를 누르면 달리기 가능
        // 달리기는 forward 방향으로만 가능하다.
        // 앞 뒤를 둘 다 누르고 있을 경우 비활성화 한다
        runFlag = moveFlag[(int)moveDir.Up] && !moveFlag[(int)moveDir.Down] && Input.GetKey(KeyCode.LeftShift);
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

    void UpdateAnim()
    {
        // 각 상태에 따라 다른 애니메이션 재생
        bool movingUp      = moveFlag[(int)moveDir.Up] && !moveFlag[(int)moveDir.Down];
        bool movingDown    = moveFlag[(int)moveDir.Down] && !moveFlag[(int)moveDir.Up];
        bool movingStrafe  = moveFlag[(int)moveDir.Right] != moveFlag[(int)moveDir.Left];
        bool strafingLeft  = moveFlag[(int)moveDir.Left] && !moveFlag[(int)moveDir.Right];
        bool strafingRight = moveFlag[(int)moveDir.Right] && !moveFlag[(int)moveDir.Left];
        bool walking       = movingUp || movingDown || movingStrafe;
        bool running       = runFlag;

        anim.SetBool("IsWalking", walking);
        anim.SetBool("IsRunning", running);

        legAnim.SetBool("IsWalking", walking);
        legAnim.SetBool("IsRunning", running);
        legAnim.SetBool("IsStrafeLeft", strafingLeft);
        legAnim.SetBool("IsStrafeRight", strafingRight);

        // 다리 애니메이션은 몸통 애니메이션과 재생 시간 동기화
        var animState = anim.GetCurrentAnimatorStateInfo(0);

        if (walking)
            legAnim.Play("IsWalking", 0, animState.normalizedTime);
        if (running)
            legAnim.Play("IsRunning", 0, animState.normalizedTime);
        if (strafingLeft)
            legAnim.Play("IsStrafeLeft", 0, animState.normalizedTime);
        if (strafingRight)
            legAnim.Play("IsStrafeRight", 0, animState.normalizedTime);
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
