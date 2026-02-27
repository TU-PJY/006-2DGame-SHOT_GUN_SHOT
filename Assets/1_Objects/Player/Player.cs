using System;
using Unity.VisualScripting;
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
    public NearAttackBound nearAttackBound;
    public Animator anim;
    public Animator legAnim;
    public float linearDamping;
    public float accSpeed;
    public float runSpeedRatio;
    public float totalHP;
    public float currHP;

    public float nearAttackDamage;

    public Shotgun shotgun;

    private bool[] moveFlag = new bool[]{false, false, false, false};
    private Vector2 force;
    private float rotation;

    private bool nearAttackState = false;
    private bool nearAttackAvailable = true;
    private float nearAttackDelay = 0f;

    // MainCamera->CameraController에서 참조
    [HideInInspector]
    public bool runFlag = false;

    void Awake()
    {
        rigidBody.linearDamping = linearDamping;
        currHP = totalHP;
        St_HPIndicator.Inst.InputHP(currHP);
    }

    void Start()
    {
        // 마우스 고정 후 숨김
        St_MouseManager.Inst.LockCursor();

        // 총기 리셋 후 시작
        if (shotgun != null)
            shotgun.ResetState();
    }

    void Update()
    {
        // 키 입력과 마우스 입력은 업데이트 일시 정지 상태에서도 계속 받도록 한다
        InputMouse();
        InputKey();

        if(!St_UpdateManager.Inst.IsRunning()) {
            anim.speed = 0f;
            legAnim.speed = 0f;
            return;
        }

        anim.speed = 1f;   
        legAnim.speed = 1f;

        UpdateAcc();
        UpdateAnim();

        if (shotgun != null)
        {
            // 소지한 총에 위치, 회전, 오프셋 전달
            shotgun.InputPositionAndRotation(rigidBody.position, rotation, new Vector2(4f, 0f));

            // 소지한 총의 전체적인 동작 업데이트
            shotgun.UpdateShotgun();
        }

        // 근접 공격 딜레이가 지나면 근접 공격 가능 상태가 활성화 된다.
        nearAttackDelay -= Time.deltaTime;
        if(nearAttackDelay <= 0f) {
            nearAttackDelay = 0f;
            nearAttackAvailable = true;
        }
    }

    private void LateUpdate()
    {
        if(!St_UpdateManager.Inst.IsRunning()) 
            return;

        // 카메라에 달리기 여부 전달
        St_CameraController.Inst.InputRunState(runFlag);

        // 카메라에 회전값 전달
        St_CameraController.Inst.InputPlayerRotation(rotation);

        // 카메라에 위치값 전달
        St_CameraController.Inst.InputPlayerPos(rigidBody.position);
    }

    void FixedUpdate()
    {
        if(!St_UpdateManager.Inst.IsRunning()) 
            return;

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

        if(!St_UpdateManager.Inst.IsRunning())
            return;

        // R키 누를 시 현재 가지고 있는 샷건 재장전 활성화
        if(shotgun != null && Input.GetKeyDown(KeyCode.R))
            shotgun.StartReload();
    }

    void InputMouse()
    {
        // 근접 공격 도중에는 총을 발사할 수 없다.
        if (!nearAttackState) {
            // 우 클릭 시 총으로 때리는 근접 공격을 한다
            if(!nearAttackState && nearAttackAvailable && Input.GetMouseButton(1)) {
                nearAttackState = true;
                nearAttackAvailable = false;
                nearAttackDelay = 1f;
            }

            // 좌 클릭 시 현재 가지고 있는 샷건 발사
            else if (Input.GetMouseButtonDown(0))
                shotgun.PullTrigger();

            // 좌 클릭 뗄 시 현재 가지고 있는 샷건 발사 중단
            else if (Input.GetMouseButtonUp(0))
                shotgun.ReleaseTrigger();
        }

        if(!St_UpdateManager.Inst.IsRunning()) // 업데이트 일시 정지 시 회전 값을 업데이트 하지 않는다.
            return;

        // 마우스 회전 시 바디 회전
        rotation -= Input.mousePositionDelta.x * St_MouseManager.Inst.sensivity;

        // 0 ~ 360도 사이에서만 회전하도록 클램프
        rotation = (rotation + 360f) % 360f;
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

        anim.SetBool("IsWalking", walking && !nearAttackState);
        anim.SetBool("IsRunning", running && !nearAttackState);
        anim.SetBool("IsNearAttacking", nearAttackState);

        legAnim.SetBool("IsWalking", walking);
        legAnim.SetBool("IsRunning", running);
        legAnim.SetBool("IsStrafeLeft", strafingLeft);
        legAnim.SetBool("IsStrafeRight", strafingRight);
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
        rigidBody.position = Math_.Clamp(rigidBody.position, new Vector2(-25f, -25f), new Vector2(25f, 25f));  
    }

    public void GiveDamage(float val)
    {
        // 몬스터에게 피격 당하면 카메라에 흔들림을 추가하고 인디케이터에 현재 체력 값을 전달한다
        currHP -= val * St_LevelManager.Inst.armorDiff;
        currHP = Mathf.Clamp(currHP, 0f, 99999f);
        St_CameraController.Inst.AddShake(0.5f);
        St_HPIndicator.Inst.InputHP(currHP);
        St_DamageIndicator.Inst.Enable();

        if(currHP <= 0f)
            St_GameOverUI.Inst.Enable();
    }

    // 애니메이터 이벤트로 호출
    public void AnimEvent_OnNearAttack()
    {
        nearAttackBound.ProcessNearAttack(nearAttackDamage);
    }

    public void AnimEvent_EndNearAttack()
    {
        nearAttackState = false;
    }
}
