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
        line.positionCount = 2;
        line.startColor = new Color(0f, 1f, 0f, 1f);
        line.endColor = new Color(0f, 1f, 0f, 1f);
        SetPosition();
    }

    void Update()
    {
        if(!St_UpdateManager.Inst.IsRunning())
            return;
        
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
