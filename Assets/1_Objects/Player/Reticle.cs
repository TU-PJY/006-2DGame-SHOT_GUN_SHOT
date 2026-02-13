using UnityEngine;

public class Reticle : MonoBehaviour
{
    public LineRenderer line;
    public float lineWeight;
    public GameObject player;
    public float offsetStart;
    public float offsetEnd;
    public float xOffset;
    
    void Awake()
    {
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = 2;
        var startPos = player.transform.position + player.transform.right * offsetStart - player.transform.up * xOffset;
        var endPos = player.transform.position + player.transform.right * offsetEnd - player.transform.up * xOffset;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    void Update()
    {
        var startPos = player.transform.position + player.transform.right * offsetStart - player.transform.up * xOffset;
        var endPos = player.transform.position + player.transform.right * offsetEnd - player.transform.up * xOffset;
        line.startWidth = lineWeight;
        line.endWidth = lineWeight;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
}
