using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public MonsterPool pool;
    public float initialMonsterCount;
    public float generateInterval;

    private float currentTime;

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
        // 일단은 테스트를 위해 좁은 범위에서 대충 생성해본다.
        inst.transform.position = new Vector2(Random.Range(-20, 20f), Random.Range(20f, 20f));
    }
}
