using UnityEngine;
using UnityEngine.UI;

public class IconAnimation_ : MonoBehaviour
{
    public Image img;

    private Vector2 originScale;
    private float imgScale = 1f;
    private float destScale = 1f;
    private Color imgColor;
    private float imgColorScale = 1f;
    private float destColorScale = 1f;

    void Awake()
    {
        imgColor = img.color;
        originScale = img.transform.localScale;
    }

    void Update()
    {
        imgScale = Mathf.Lerp(imgScale, destScale, Time.deltaTime * 5f);
        imgColorScale = Mathf.Lerp(imgColorScale, destColorScale, Time.deltaTime * 10f);
        img.transform.localScale = new Vector2(originScale.x * imgScale, originScale.y * imgScale);
        img.color = new Color(imgColor.r * imgColorScale, imgColor.g * imgColorScale, imgColor.b * imgColorScale);
    }

    // 클릭하면 아이콘 이미지가 축소된다.
    public void OnCursor()
    {
        destScale = 1.3f;
    }

    public void OffCursor()
    {
        destScale = 1f;
    }

    public void DownCursor()
    {
        destColorScale  = 0.5f;
    }

    public void UpCursor()
    {
        destColorScale = 1f;
    }
}
