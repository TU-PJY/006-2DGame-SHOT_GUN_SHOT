using System;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int   maxAmmo; // 최대 장탄수
    public int   pelletCount; // 펠릿 개수
    public int   pelletDamage; // 펠릿 당 대미지
    public float pelletDisperse; // 펠릿 퍼짐 수치 // 클 수록 더 넓게 퍼짐
    public float maxDistance; // 펠릿이 대미지를 줄 수 있는 거리
    public float fireInterval; // 발사 간격
    public float pelletReloadInterval; // 펠릿 당 장전 시간
    public float recoil; // 반동 // 클 수록 카메라가 더 많이 흔들림

    private bool triggerState; // 방아쇠당긴 상태
    private bool reloadState; // 재장전 상태 // Fire 실행 시 reloadState 취소
    private float currentFireIntervalTime; // 현재 발사 간격 측정 누적 시간
    private float currentReloadTime ; // 현재 재장전 간격 측정 누적 시간
    private int currentAmmo; // 현재 장탄 수

    public void ResetState()
    {
        currentAmmo = maxAmmo;
        triggerState = false;
        reloadState = false;
        currentFireIntervalTime = 0f;
        currentReloadTime = 0f;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void PullTrigger()
    {
        triggerState = true;
        reloadState = false; // 재장전 중이었다면 재장전 중단
        currentReloadTime = pelletReloadInterval;
    }

    public void ReleaseTrigger()
    {
        triggerState = false;
    }

    public void StartReload()
    {
        if (currentAmmo < maxAmmo)
        {
            reloadState = true;
            triggerState = false; // 발사 중이었다면 발사 중단
        }
    }

    public void UpdateShotgun()
    {
        UpdateFireInterval(); // 발사 딜레이 업데이트

        if (triggerState) // 방아쇠를 당긴 상태라면 발사 업데이트
            UpdateFire();

        if (reloadState) // 재장전 상태라면 재장전 업데이트
            UpdateReload();
    }

    private void UpdateFireInterval()
    {
        currentFireIntervalTime -= Time.deltaTime;
        if(currentFireIntervalTime <= 0f)
            currentFireIntervalTime = 0f;
    }

    private void UpdateFire()
    {
        if (currentAmmo == 0) // 탄약이 모두 떨어지면 발사 중지
            return;

        if (currentFireIntervalTime <= 0f)
        {
            currentAmmo--;
            CameraController.Inst.AddShake(recoil); // 카메라에 흔들림 추가
            currentFireIntervalTime += fireInterval; // 발사 간격 시간 값을 더하여 다음 발사 준비
        }
    }

    private void UpdateReload()
    {
        currentReloadTime -= Time.deltaTime;

        // 탄약을 모두 다 장전했다면 재장전 상태를 비활성화 하고 아니라면 다음 탄약 장전을 준비
        if (currentReloadTime <= 0f)
        {
            currentAmmo++;

            if (currentAmmo == maxAmmo)
            {
                currentReloadTime = 0f;
                reloadState = false;
            }
            else
                currentReloadTime += pelletReloadInterval;
        }
    }
}
