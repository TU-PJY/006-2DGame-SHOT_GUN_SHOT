using UnityEngine;

// 인게임 오브젝트의 업데이트 일시정지/시작을 담당하는 모듈
// 모든 움직이는 오브젝트들은 이 모듈의 값을 참조하도록 한다
public class St_UpdateManager : MonoBehaviour
{
    public static St_UpdateManager Inst;
    private bool UpdateState = true;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        print("[UpdateManager] Created instance.");
    }

    void OnDestroy()
    {
        Inst = null;
    }

    // 업데이트 일시정지
    public void Pause()
    {
        UpdateState = false;
        print("[UpdateManager] Paused update.");
    }

    // 업데이트 재개
    public void Resume()
    {
        UpdateState = true;
        print("[UpdateManager] Resumed update.");
    }

    // 업데이트 상태 확인
    public bool Check()
    {
        return UpdateState;
    }
}
