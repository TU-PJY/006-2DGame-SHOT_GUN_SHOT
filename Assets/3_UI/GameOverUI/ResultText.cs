using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public Text text;
    private string originString;

    void Awake()
    {
        originString = text.text;
    }

    public void UpdateText()
    {
        var bestRounds = PlayerPrefs.GetInt("SGS_BestRounds");
        var currRounds = St_GameManager.Inst.currentRound;
        var modified = originString;
        modified = modified.Replace("{1}", currRounds.ToString());
        modified = modified.Replace("{2}", bestRounds.ToString());

        // 최고기록을 갱신했다면 최고 기록을 갱신했음을 알리고 새로운 값을 데이터에 쓴다.
        if(currRounds > bestRounds)
        {
            modified += "\n<size=60>Best Rounds!</size>";
            PlayerPrefs.SetInt("SGS_BestRounds", currRounds);
        }

        text.text = modified;
    }
}
