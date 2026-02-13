using UnityEngine;

public class Reticle : MonoBehaviour
{
    public LineRenderer line;
    public float lineWeight;
    public GameObject player;
    public float startOffsetY;
    public float endOffsetY;
    public float centerOffsetX;
    public float startOffsetX;
    public float endOffsetX;

    void Awake()
    {
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = 2;
        SetPosition();
    }

    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        var startPos = player.transform.position + player.transform.right * startOffsetY - player.transform.up * (centerOffsetX + startOffsetX);
        var endPos   = player.transform.position + player.transform.right * endOffsetY - player.transform.up * (centerOffsetX + endOffsetX);
        line.startWidth = lineWeight;
        line.endWidth = lineWeight;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
}
