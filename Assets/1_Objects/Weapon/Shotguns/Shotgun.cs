using System;
using UnityEngine;
using UnityEngine.UIElements;
using T = MatrixTransform;

public class Shotgun : MonoBehaviour
{
    public int   maxAmmo; // 최대 장탄수
    public int   pelletCount; // 펠릿 개수
    public float pelletDamage; // 펠릿 당 대미지
    public float pelletDisperse; // 펠릿 퍼짐 수치 // 클 수록 더 넓게 퍼짐
    public float pelletDistance; // 펠릿이 대미지를 줄 수 있는 거리
    public float fireInterval; // 발사 간격
    public float pelletReloadInterval; // 펠릿 당 장전 시간
    public float recoil; // 반동 // 클 수록 카메라가 더 많이 흔들림
    public float muzzleFireAnimSpeed; // 충구 화염 애니메이션 속도
    public int startTotalAmmoCount; // 시작 탄약 개수

    private bool triggerState; // 방아쇠당긴 상태
    private bool reloadState; // 재장전 상태 // Fire 실행 시 reloadState 취소
    private float currentFireIntervalTime; // 현재 발사 간격 측정 누적 시간
    private float currentReloadTime ; // 현재 재장전 간격 측정 누적 시간
    private int currentAmmo; // 현재 장탄 수
    private int currentTotalAmmo; // 현재 소지 탄약 개수

    private Vector2 playerPos;
    private float playerRotation;
    private Vector2 playerOffset;

    public void ResetState()
    {
        currentAmmo = maxAmmo;
        triggerState = false;
        reloadState = false;
        currentFireIntervalTime = 0f;
        currentReloadTime = 0f;
        currentTotalAmmo = startTotalAmmoCount;
        St_BulletCountIndicator.Inst.InputBulletCount(currentAmmo); // UI에 현재 장탄수 반영
        St_BulletCountIndicator.Inst.InputTotalAmmoCount(currentTotalAmmo);
    }

    public void InputPositionAndRotation(Vector2 position, float degrees, Vector2 offset)
    {
        playerPos = position;
        playerRotation = degrees;
        playerOffset = offset;
    }

    public void PullTrigger()
    {
        triggerState = true;
        StopReload(); // 재장전 중이었다면 재장전 중단
    }

    public void ReleaseTrigger()
    {
        triggerState = false;
    }

    public void StartReload()
    {
        // 가지고 있는 탄약이 있어야 재장전 가능하다
        if (currentAmmo < maxAmmo && currentTotalAmmo > 0)
        {
            reloadState = true;
            triggerState = false; // 발사 중이었다면 발사 중단
        }
    }

    public void StopReload()
    {
        reloadState = false;
        St_ReloadIndicator.Inst.SetInvisible(); // UI 비활성화
        currentReloadTime = pelletReloadInterval * St_LevelManager.Inst.reloadSpeedDiff;
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

            St_BulletCountIndicator.Inst.InputBulletCount(currentAmmo); // UI에 현재 장탄수 반영
            St_CameraController.Inst.AddShake(recoil); // 카메라에 흔들림 추가

            // 총구 위치에 새로운 총구 화염 배치
            var muzzleFire = St_ObjectManager.Inst.GetMuzzleFire(); // 총구 화염 오브젝트 생성
            Matrix4x4 muzzleMatrix = new();
            T.Identity(ref muzzleMatrix);
            T.Translate(ref muzzleMatrix, playerPos);
            T.Rotate(ref muzzleMatrix, playerRotation);
            T.Translate(ref muzzleMatrix, playerOffset);
            T.Scale(ref muzzleMatrix, new Vector2(0.3f, 0.3f));
            T.Dispatch(muzzleFire.transform, ref muzzleMatrix);
            muzzleFire.SetAnimSpeed(muzzleFireAnimSpeed);

            // 총구 위치에 새로운 펠릿(ray) 배치
            var pellet = St_PelletManager.Inst;
            T.Translate(ref muzzleMatrix, new Vector2(-playerOffset.x * 1.5f, 0f));
            T.Dispatch(pellet.transform, ref muzzleMatrix);
            pellet.RayCast(pelletCount + St_LevelManager.Inst.pelletDiff, pelletDisperse, pelletDistance, pelletDamage);

            currentFireIntervalTime += fireInterval * St_LevelManager.Inst.shootSpeedDiff; // 발사 간격 시간 값을 더하여 다음 발사 준비
        }
    }

    private void UpdateReload()
    {
        currentReloadTime -= Time.deltaTime;

        // 재장전 인디케이터에 총 재장전 시간과 현재 재장전 시간 입력
        St_ReloadIndicator.Inst.InputReloadTime(pelletReloadInterval * St_LevelManager.Inst.reloadSpeedDiff, currentReloadTime);

        // 탄약을 모두 다 장전했다면 재장전 상태를 비활성화 하고 아니라면 다음 탄약 장전을 준비
        if (currentReloadTime <= 0f)
        {
            currentAmmo++;
            currentTotalAmmo--;
            St_BulletCountIndicator.Inst.InputBulletCount(currentAmmo); // UI에 현재 장탄수 반영
            St_BulletCountIndicator.Inst.InputTotalAmmoCount(currentTotalAmmo);
            
            // 최대 장탄수까지 장전하거나 소지 탄약을 모두 사용하면 재장전 중단
            if (currentAmmo == maxAmmo || currentTotalAmmo == 0)
            {
               StopReload();
            }
            else
                currentReloadTime += pelletReloadInterval * St_LevelManager.Inst.reloadSpeedDiff;
        }
    }
}
