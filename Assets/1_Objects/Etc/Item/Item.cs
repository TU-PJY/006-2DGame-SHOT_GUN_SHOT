using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;

    public float knockbackForce;

    public virtual void ReturnInstance()
    {
        
    }

    // 아이템 생성 시 랜덤 회전 지정 후 마지막으로 맞은 총알 방향으로 힘을 가함
    public void SetState(Vector2 position, Vector2 direction)
    {
        var randomRot = Random.Range(0f, 180f);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
        gameObject.transform.position = position;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(knockbackForce * direction, ForceMode2D.Impulse);
    }
}