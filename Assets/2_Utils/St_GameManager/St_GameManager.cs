using UnityEngine;

public class St_GameManager : MonoBehaviour
{
    public static St_GameManager Inst;
    public float mouseSensivity;
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
        if(Inst && Inst == this)
        {
            print("[GameManager] Instance is exist already. Destroy duplicated instance.");
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[GameManager] Created new instance.");
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
        if(!St_UpdateManager.Inst.Check())
            return;

        if(remainedEnemy == 0) { // 한 라운드가 지날 때마다 목표 적 개수가 증가한다.
            currentRound++;
            destEnemyCount += enemyIncrease;
            remainedEnemy = destEnemyCount;
            St_MonsterGenerator.Inst.SetNextRound(); // 제너레이터에게 다음 라운드로 넘어갔음을 알린다
            St_RoundIndicator.Inst.InputRound(currentRound); // 라운드 인디케이터에 변경된 라운드 전달
            St_RemainEnemyIndicator.Inst.InputRemainedEnemy(remainedEnemy); // 남은 적 개수 갱신

            print($"[GameManager] Next round started. | Round: {currentRound} | Dest enemy count: {destEnemyCount}");
        }
    }

    public void Release()
    {
        print("[GameManager] Released instance.");
        Inst = null;
    }
}
