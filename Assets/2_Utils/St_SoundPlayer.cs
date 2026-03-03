using UnityEngine;

public class St_SoundPlayer : MonoBehaviour
{
    public static St_SoundPlayer Inst;
    public AudioSource audioSrc;

    public AudioClip shootClip;
    public AudioClip reloadClip;
    public AudioClip nearHitClip;
    public AudioClip playerFootstepClip;
    public AudioClip playerHitClip;
    public AudioClip nearAttackClip;
    public AudioClip playerDeathClip;

    public AudioClip monsterDeathClip;
    public AudioClip monsterBigDeathClip;

    public AudioClip ammoGetClip;
    public AudioClip healthGetGlip;
    
    public AudioClip roundStartClip;
    public AudioClip buttonClip;

    void Awake()
    {
        if(Inst && Inst != this)
        {
            DestroyImmediate(this);
            return;
        }

        Inst = this;
        DontDestroyOnLoad(gameObject);
    }

    // 샷건 발사 사운드
    public void PlayShootSound()
    {
        audioSrc.PlayOneShot(shootClip);
    }

    // 재장전 사운드
    public void PlayReloadSound()
    {
        audioSrc.PlayOneShot(reloadClip);
    }

    // 근접공격 때리기 사운드
    public void PlayNearHitSound()
    {
        audioSrc.PlayOneShot(nearHitClip);
    }

    // 플레이어 발소리 사운드
    public void PlayPlayerFootstepSound()
    {
        audioSrc.PlayOneShot(playerFootstepClip);
    }

    // 플레이어 사망 사운드
    public void PlayPlayerDeathSound()
    {
        audioSrc.PlayOneShot(playerDeathClip);
    }

    // 플레이어 피격 사운드
    public void PlayPlayerHit()
    {
        audioSrc.PlayOneShot(playerHitClip);
    }

    // 큰 몬스터 죽는 사운드
    public void PlayBigMonsterDeathSound()
    {
        audioSrc.PlayOneShot(monsterBigDeathClip);
    }

    // 일반 몬스터 죽는 사운드
    public void PlayMonsterDeathSound()
    {
        audioSrc.PlayOneShot(monsterDeathClip);
    }

    // 플레이어 근접 공격 사운드
    public void PlayPlayerNearAttackSound()
    {
        audioSrc.PlayOneShot(nearAttackClip);
    }

    // 총알 획득 사운드
    public void PlayAmmoGetSound()
    {
        audioSrc.PlayOneShot(ammoGetClip);
    }

    // 회봉아이템 획득 사운드
    public void PlayHealthGetSound()
    {
        audioSrc.PlayOneShot(healthGetGlip);
    }

    // 라운드 시작 사운드
    public void PlayRoundStartSound()
    {
        audioSrc.PlayOneShot(roundStartClip);
    }

    // 버튼 클릭 사운드
    public void PlayButtonSound()
    {
        audioSrc.PlayOneShot(buttonClip);
    }
}
