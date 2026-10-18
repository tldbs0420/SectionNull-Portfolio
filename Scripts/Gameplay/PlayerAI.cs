using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{

    [Header("AI Setting")]
    [SerializeField] Animator animator;
    [SerializeField] public IShooter shooter { get; private set; }
    [SerializeField] private float shootYOffset = 1f;

    [SerializeField] PlayerWeaponConfig weaponConfig;
    [SerializeField] AudioClip shootSound;
    [SerializeField] FireEffect fireEffect;
    [SerializeField] float reloadTime;
    [SerializeField] private Transform modelRoot;

    List<Enemy> enemies;
    [Header("Debug")]
    [SerializeField] private Enemy currEnemy;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private int curAmmo;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] WeaponGrade currentGrade;
    [field: SerializeField] public float BonusFireInterval {get; private set;}
    [field: SerializeField] public int BonusMaxMag {get; private set;}
    [field: SerializeField] public int BonusAttack {get; private set;}

    private String[] triggers = {"GunFire", "Reload"};
    private AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (!enemies.Contains(enemy))
                enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemies.Contains(enemy))
                enemies.Remove(enemy);
            if (enemies.Count > 0)
                currEnemy = enemies[0];
            else
                currEnemy = null;
        }  
    }

    public void Init()
    {
        shooter = GetComponent<IShooter>();
        enemies = new List<Enemy>();
        curAmmo = weaponConfig.MagSize;
        currentGrade = WeaponGrade.Common;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(KillEnemy());
    }

    void Update()
    {
        if (enemies != null)
        {
            enemies.RemoveAll(e => e == null || !e.gameObject.activeInHierarchy || e.GetComponent<Enemy>().IsDead());
            if (enemies.Count > 0) currEnemy = enemies[0];
            else currEnemy = null;

            if (currEnemy != null)
            {
                Vector3 dir = currEnemy.transform.position - transform.position;
                dir.y = 0f; // ← 수평 방향만 사용 (pitch 제거)

                // if (dir.sqrMagnitude > 0.0001f)
                // {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRot,
                    rotateSpeed * Time.deltaTime
                );
                // }
            }
            else
            {
                animator.SetBool(triggers[0], false);
            }
        }
    }

    void LateUpdate()
    {
        // 애니메이션 다 적용된 뒤에, 모델의 로컬 Y 회전을 정리
        if (modelRoot == null) return;

        Vector3 euler = modelRoot.localEulerAngles;
        euler.y = 0f;   // 필요하면 x,z도 0으로
        modelRoot.localEulerAngles = euler;
    }

    public void SetGrade(WeaponGrade grade)
    {
        if (currentGrade < grade)
        {
            currentGrade = grade;
        
            var BaseStat = weaponConfig.weaponStatTable.GetStats(WeaponGrade.Common);
            var AdaptedStat = weaponConfig.weaponStatTable.GetStats(currentGrade);
            
            // 데미지 스텟 적용 
            BonusAttack = AdaptedStat.attack;
            shooter.BonusDamage = AdaptedStat.attack;

            // 탄창 스텟 적용
            BonusMaxMag = AdaptedStat.magazine - BaseStat.magazine;

            // 연사력 스텟 적용
            BonusFireInterval = weaponConfig.FireInterval * (0.0025f * (AdaptedStat.fireRate - BaseStat.fireRate));    
            }
    }

    IEnumerator KillEnemy()
    {
        while (true)
        {
            if (currEnemy != null && !isReloading)
            {
                animator.SetBool(triggers[0], true);
                Vector3 target = currEnemy.transform.position;
                float currOffset;
                if (currEnemy.GetComponent<MinionZombie>() != null)
                    currOffset = 0.5f;
                else
                    currOffset = shootYOffset;

                target = new Vector3(target.x, target.y + currOffset, target.z);

                var bullet = shooter.ImmediateShootProjectile(target);
                curAmmo -= 1;
                audioSource.PlayOneShot(shootSound);
                fireEffect.PlayEffect();
                yield return new WaitForSeconds(weaponConfig.FireInterval - BonusFireInterval);
                
                if (curAmmo <= 0 && !isReloading) StartCoroutine(Reload());
            }
            yield return null;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool(triggers[1], true);
        yield return new WaitForSeconds(reloadTime);
        curAmmo = weaponConfig.MagSize + BonusMaxMag;
        animator.SetBool(triggers[1], false);
        isReloading = false;
    }

    public AudioSource GetAudioSource() => audioSource;
}
