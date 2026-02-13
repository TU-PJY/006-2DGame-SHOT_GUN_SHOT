using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public MonsterPool pool;
    public float initialMonsterCount;
    public float generateInterval;

    private float currentTime;

    void Start()
    {
        for(int i  = 0; i < initialMonsterCount; i ++)
            UpdateGenerateOperation();
    }

    void Update()
    {
        UpdateGenerateOperation();
    }

    void UpdateGenerateOperation()
    {
        // 델타 시간을 더해 실제 타이머처럼 사용
        currentTime += Time.deltaTime;
        if(currentTime >= generateInterval)
        {
            var inst = pool.GetMonster();
            inst.transform.position = new Vector2(Random.Range(0f, 0f), Random.Range(1f, 1f));
        }
    }
}
