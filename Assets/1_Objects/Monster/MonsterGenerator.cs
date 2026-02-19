using UnityEngine;

// 맵 끝에서 스폰
public enum SpawnDir
{
    Left,
    Right,
    Top,
    Bottom
}

public class MonsterGenerator : MonoBehaviour
{
    public MonsterPool pool;
    public float initialMonsterCount;
    public float generateInterval;
    public Vector2 mapSize;

    private float currentTime;
    private SpawnDir[] spawnDir = new SpawnDir[4]{ SpawnDir.Left, SpawnDir.Right, SpawnDir.Top, SpawnDir.Bottom };

    void Start()
    {
        for (int i = 0; i < initialMonsterCount; i++)
            GenerateOperation();
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
        }
    }

    void GenerateOperation()
    {
        var inst = pool.GetMonster();

        // 맵의 가장자리 4방향 중 하나를 선택
        var spawnEdge = spawnDir[Random.Range(0, 3)];

        switch (spawnEdge)
        {
            case SpawnDir.Left: // 맵의 왼쪽에서 스폰
                inst.transform.position = new Vector2(-mapSize.x, Random.Range(-mapSize.y, mapSize.y));
                break;
            case SpawnDir.Right: // 맵의 오른쪽에서스폰
                inst.transform.position = new Vector2(mapSize.x, Random.Range(-mapSize.y, mapSize.y));
                break;
            case SpawnDir.Top: // 맵의 상단에서 스폰
                inst.transform.position = new Vector2(Random.Range(-mapSize.x, mapSize.x), mapSize.y);
                break;
            case SpawnDir.Bottom: // 맵의 하단에서 스폰
                inst.transform.position = new Vector2(Random.Range(-mapSize.x, mapSize.x), -mapSize.y);
                break;
        }
    }
}
