using UnityEngine;

[ExecuteInEditMode] // 에디터 모드에서도 보이게 합니다.
public class ScreenCenterLine : MonoBehaviour
{
    void OnGUI()
    {
        float thickness = 2f; // 선 두께
        float lineLength = 1000f; // 선 길이 (화면 전체는 Screen.width 사용)
        
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        GUI.color = Color.green;

        // 가로선
        GUI.DrawTexture(new Rect(center.x - lineLength / 2, center.y - thickness / 2, lineLength, thickness), Texture2D.whiteTexture);
        // 세로선
        GUI.DrawTexture(new Rect(center.x - thickness / 2, center.y - lineLength / 2, thickness, lineLength), Texture2D.whiteTexture);
    }
}