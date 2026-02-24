using System;
using UnityEngine;

enum moveDir
{
    Up,
    Down,
    Right,
    Left
}

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator anim;
    public Animator legAnim;
    public float linearDamping;
    public float accSpeed;
    public float runSpeedRatio;

    public Shotgun shotgun;

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

        // 총기 리셋 후 시작
        if (shotgun != null)
            shotgun.ResetState();
    }

    void Update()
    {
        InputMouse();
        InputKey();
        UpdateAcc();
        UpdateAnim();

        if (shotgun != null)
        {
            // 소지한 총에 위치, 회전, 오프셋 전달
            shotgun.InputPositionAndRotation(rigidBody.position, rotation, new Vector2(4f, 0f));

            // 소지한 총의 전체적인 동작 업데이트
            shotgun.UpdateShotgun();
        }
    }

    private void LateUpdate()
    {
        // 카메라에 달리기 여부 전달
        CameraController.Inst.InputRunState(runFlag);

        // 카메라에 회전값 전달
        CameraController.Inst.InputPlayerRotation(rotation);

        // 카메라에 위치값 전달
        CameraController.Inst.InputPlayerPos(rigidBody.position);
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

        // R키 누를 시 현재 가지고 있는 샷건 재장전 활성화
        if(shotgun != null && Input.GetKeyDown(KeyCode.R))
            shotgun.StartReload();
    }

    void InputMouse()
    {
        // 마우스 회전 시 바디 회전
        rotation -= Input.mousePositionDelta.x * GameManager.Inst.mouseSensivity;

        // 0 ~ 360도 사이에서만 회전하도록 클램프
        rotation = (rotation + 360f) % 360f;

        // 좌 클릭 시 현재 가지고 있는 샷건 발사
        if (shotgun != null && Input.GetMouseButtonDown(0))
            shotgun.PullTrigger();

        // 좌 클릭 뗄 시 현재 가지고 있는 샷건 발사 중단
        else if (shotgun != null && Input.GetMouseButtonUp(0))
            shotgun.ReleaseTrigger();
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
        bool running       = runFlag && walking;

        anim.SetBool("IsWalking", walking);
        anim.SetBool("IsRunning", running);

        legAnim.SetBool("IsWalking", walking);
        legAnim.SetBool("IsRunning", running);
        legAnim.SetBool("IsStrafeLeft", strafingLeft);
        legAnim.SetBool("IsStrafeRight", strafingRight);

        // 특정 상태가 현재 활성화 되었는지 확인
        Predicate<string> checkState = (name) => legAnim.GetCurrentAnimatorStateInfo(0).IsName(name);

        // 다리 애니메이션은 몸통 애니메이션과 재생 시간 동기화
        var animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (walking && checkState("IsWalking"))
            legAnim.Play("IsWalking", 0, animTime);
        if (running && checkState("IsRunning"))
            legAnim.Play("IsRunning", 0, animTime);
        if (strafingLeft && checkState("IsStrafeLeft"))
            legAnim.Play("IsStrafeLeft", 0, animTime);
        if (strafingRight && checkState("IsStrafeRight"))
            legAnim.Play("IsStrafeRight", 0, animTime);
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
