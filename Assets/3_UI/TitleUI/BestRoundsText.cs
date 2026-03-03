using UnityEngine;
using UnityEngine.UI;

public class BestRoundsText : MonoBehaviour
{
    public Text text;
    private string originString;

    public void Start()
    {
        originString = text.text;
        UpdateText();
    }

    public void UpdateText()
    {
        var modified = originString;
        modified = modified.Replace("{}", PlayerData.LoadBestRoundsData().ToString());
        text.text = modified;
    }
}
