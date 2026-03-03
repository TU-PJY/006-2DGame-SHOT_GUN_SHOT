using UnityEngine;

public class PlayerLeg : MonoBehaviour
{
    public void OnFootstep()
    {
        St_SoundPlayer.Inst.PlayPlayerFootstepSound();
    }
}
