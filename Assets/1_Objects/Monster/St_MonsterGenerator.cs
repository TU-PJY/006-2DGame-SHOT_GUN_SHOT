using UnityEngine;

// 맵 끝에서 스폰
public enum SpawnDir
{
    Left,
    Right,
    Top,
    Bottom
}

public class St_MonsterGenerator : MonoBehaviour
{
    public static St_MonsterGenerator Inst;
    
    public float initialMonsterCount;
    public float generateInterval;
    public Vector2 mapSize;

    private float currentTime;
    private SpawnDir[] spawnDir = new SpawnDir[4]{ SpawnDir.Left, SpawnDir.Right, SpawnDir.Top, SpawnDir.Bottom };
    private int generateCount;

    void Awake()
    {
        if(Inst && Inst == this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[MonsterGenerator] Created instance.");
    }

    void Start()
    {
        for (int i = 0; i < initialMonsterCount; i++)
            GenerateOperation();
    }

    public void DisableGeneration()
    {
        gameObject.SetActive(false);
    }

    // 다음 라운드로 넘어가면 약간의 딜레이 뒤 스폰하도록 한다.
    // 스폰 딜레이는 일정 수치만큼 줄어든다.
    public void SetNextRound()
    {
        gameObject.SetActive(true);
        generateCount = 0;
        currentTime = -3f;
        generateInterval -= 0.2f; // 0.2씩 스폰 간격 감소
        generateInterval = Mathf.Clamp(generateInterval, 0.2f, 100f); // 0.2초 미만으로는 감소하되지 않도록 한다.
    }

    void Update()
    {
        TimerOperation();
    }

    void TimerOperation()
    {
        // 델타 시간을 더해 실제 타이머처럼 사용
        currentTime += Time.deltaTime;
        if(currentTime >= generateInterval)
        {
            GenerateOperation();
            currentTime -= generateInterval;
            generateCount++;

            // 몬스터를 목표치 만큼 생성하면 다음 라운드로 넘어가기 전까지 생성하지 않는다.
            // SetNextRound()로 다시 생성을 시작하도록 할 수 있다.
            if(generateCount == St_GameManager.Inst.destEnemyCount)
                DisableGeneration();
        }
    }

    void GenerateOperation()
    {
        var inst = St_ObjectManager.Inst.GetMonster();
        inst.ResetState();

        // 맵의 가장자리 4방향 중 하나를 선택
        var spawnEdge = spawnDir[Random.Range(0, 3)];
        var offset = 5f;

        switch (spawnEdge)
        {
            case SpawnDir.Left: // 맵의 왼쪽에서 스폰
                inst.transform.position = new Vector2(-mapSize.x - offset, Random.Range(-mapSize.y, mapSize.y));
                break;
            case SpawnDir.Right: // 맵의 오른쪽에서스폰
                inst.transform.position = new Vector2(mapSize.x + offset, Random.Range(-mapSize.y, mapSize.y));
                break;
            case SpawnDir.Top: // 맵의 상단에서 스폰
                inst.transform.position = new Vector2(Random.Range(-mapSize.x, mapSize.x), mapSize.y + offset);
                break;
            case SpawnDir.Bottom: // 맵의 하단에서 스폰
                inst.transform.position = new Vector2(Random.Range(-mapSize.x, mapSize.x), -mapSize.y - offset);
                break;
        }

        if(St_GameManager.Inst.currentRound > 1)
        {
            bool specialFlag;

            // 라운드가 올라갈 수록 특수 속성 몬스터의 생성 확률이 높아진다.
            if (Range_.InRange(St_GameManager.Inst.currentRound, 2, 10))
                specialFlag = Range_.Propability(20); 

            else if (Range_.InRange(St_GameManager.Inst.currentRound, 11, 19))
                specialFlag = Range_.Propability(40); 

            else
                specialFlag = Range_.Propability(60); 
 
            if(specialFlag) {
                // 타입 중 하나를 선택하여 특수 속성 몬스터 생성
                // 1: 빠르고 약함
                // 2: 느리고 강함
                int randomType = Random.Range(1, 3);

                switch(randomType) {
                case 1: // 약하고 빠름
                    inst.accSpeed = 64f;    
                    inst.transform.localScale = new Vector2(0.7f, 0.7f);
                    inst.attackDamage = 2;
                    inst.attackSpeed = 24f;
                    break;

                case 2: // 강하고 느림
                    inst.accSpeed = 16f;    
                    inst.transform.localScale = new Vector2(1.8f, 1.8f);
                    inst.attackDamage = 20;
                    inst.attackSpeed = 8f;
                    break;
                }
            }
        }
    }
}
