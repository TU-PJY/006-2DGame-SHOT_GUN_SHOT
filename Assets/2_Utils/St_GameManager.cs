using UnityEngine;

public class St_GameManager : MonoBehaviour
{
    public static St_GameManager Inst;
    public int startRound;
    public int startEnemyCount;
    public int enemyIncrease;

    [HideInInspector]
    public int currentRound;
    [HideInInspector]
    public int remainedEnemy;
    [HideInInspector]
    public int destEnemyCount;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            print("[GameManager] Instance is exist already. Destroy duplicated instance.");
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[GameManager] Created new instance.");
    }
    void OnDestroy()
    {
        Inst = null;
    }

    void Start()
    {
        currentRound = startRound;
        destEnemyCount = startEnemyCount + (currentRound - 1) * enemyIncrease;
        remainedEnemy = destEnemyCount;

        // 인디케이터에 정보를 먼저 전달 후 시작
        St_RemainEnemyIndicator.Inst.InputRemainedEnemy(remainedEnemy);
        St_RoundIndicator.Inst.InputRound(currentRound);
    }

    void Update()
    {
        if(!St_UpdateManager.Inst.IsRunning()) // 업그레이드 UI 활성화 상태에서는 업데이트하지 않음
            return;

        // 일시정지가 아닐 때는 St_GameManager에서 활성화하고 비활성화는 St_PauseU또는 St_PauseUI의 ResumeButton에서 한다.
        if(Input.GetKeyDown(KeyCode.Escape))
            St_PauseUI.Inst.Enable();

        // 라운드가 끝나면 인게임을 일시정지 후 업그레이드 인터페이스를 활성화 한다.
        // 만약 모든 항목의 레벨이 최고 레벨이라면 더 이상 업그레이드 인터페이스를 활성화하지 않는다.
        if(remainedEnemy == 0) {
            if (!St_LevelManager.Inst.isAllLevelMax)
            {
                St_UpgradeUI.Inst.Enable(); // 업그레이드 인터페이스 활성화
                St_InGameUI.Inst.Disable(); // 인게임 인터페이스 비활성화
                St_UpdateManager.Inst.Pause(); // 업데이트 일시 중지
                St_MouseManager.Inst.UnlockCursor(); // 커서 잠금 비활성화
            }
            else
                NextRound(); // 모든 항목이 최고 레벨일 경우 라운드만 증가한다.
        }
        
    }

    // 업그레이드 인터페이스에서 호출하여 업그레이드 인터페이스를 비활성화하고, 다음 라운드로 넘어간다.
    public void SetNextRound()
    {
        NextRound();

        St_MonsterGenerator.Inst.SetNextRound(); // 제너레이터에게 다음 라운드로 넘어갔음을 알린다
        St_RoundIndicator.Inst.InputRound(currentRound); // 라운드 인디케이터에 변경된 라운드 전달
        St_RemainEnemyIndicator.Inst.InputRemainedEnemy(remainedEnemy); // 남은 적 개수 갱신

        St_UpgradeUI.Inst.Disable(); // 업그레이드 인터페이스 비활성화
        St_InGameUI.Inst.Enable(); // 인게임 인터페이스 활성화
        St_UpdateManager.Inst.Resume(); // 업데이트 재개
        St_MouseManager.Inst.LockCursor(); // 커서 잠금 활성화
    }

    void NextRound()
    {
        currentRound++;
        destEnemyCount += enemyIncrease;
        remainedEnemy = destEnemyCount;
        print($"[GameManager] Next round started. | Round: {currentRound} | Dest enemy count: {destEnemyCount}");
    }
}
