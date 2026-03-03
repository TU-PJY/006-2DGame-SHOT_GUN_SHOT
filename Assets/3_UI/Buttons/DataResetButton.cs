using UnityEngine;

public class DataResetButton : MonoBehaviour
{
    public BestRoundsText bText;
    
    // 플레이어 데이터를 초기화 한다.
    public void OnButtonClick()
    {
        St_SoundPlayer.Inst.PlayButtonSound();
        PlayerData.ResetBestRoundsData();
        bText.UpdateText();
        print("[ResetButton] Player data reset.");
    }
}
