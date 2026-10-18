using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public PlayerMovement playerMovement;

    [Header("AI")]
    public PlayerAI[] ai;

    [Header("Managers")]
    public PoolManager poolManager;
    public DayTest dayTest;
    public EventBus eventBus;
    public CutSceneManager cutSceneManager;
    public SoundManager soundManager;
    public PlayerManager playerManager;
    public AmmoUIManager ammoUIManager;

    [Header("Spawners")]
    [SerializeField] GameObject farmSpawners;
    [SerializeField] GameObject townSpawners;
    [SerializeField] GameObject factorySpawners;
    [SerializeField] BossSpawner bossSpawner;

    [Header("CellCounts")]
    [SerializeField] GameObject farmCells;
    [SerializeField] GameObject townCells;
    [SerializeField] GameObject factoryCells;

    [Header("UI")]
    [SerializeField] OptionUI optionUI;

    [Header("Debug")]
    [SerializeField] int farmCellCount;
    [SerializeField] int townCellCount;
    [SerializeField] int factoryCellCount;

    [SerializeField] int curFarmCellCount;
    [SerializeField] int curTownCellCount;
    [SerializeField] int curFactoryCellCount;

    [SerializeField] bool _boss1 = false;
    [SerializeField] bool _boss2 = false;
    [SerializeField] bool _boss3 = false;

    [SerializeField] bool _boss1Spawned = false;
    [SerializeField] bool _boss2Spawned = false;
    [SerializeField] bool _boss3Spawned = false;

    [SerializeField] Areas _boss1Point;
    [SerializeField] Areas _boss2Point;
    [SerializeField] Areas _boss3Point;



    public enum Areas
    {
        Farm,
        Town,
        Factory,
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void InitGame(String main, String sub)
    {
        cutSceneManager.PlayCutScene(0);

        soundManager.Init();
        //playerMovement.Init();
        eventBus.Init();
        poolManager.Init();
        dayTest.Init();

        // playerManager.Init(무기이름, 무기이름) 하면 해당 무기 넣고 시작. 
        // 무기이름은 playerManager 리스트에 있는거 그대로 사용. 없으면 Dummy가 들어감
        // playerManager.Init("AKM", "D_Eagle");
        // playerManger말고 playerMovement로 이니셜라이즈 하시면, playerMovement.Init()에 GetComponent<PlayerWeaponController>().Init(); 주석처리 풀어주세요
        playerManager.Init(main, sub);
        ai[0].Init();
        ai[1].Init();

        ammoUIManager.Init();

        OptionManager.Instance.ConnectGame(soundManager, playerMovement, optionUI);
        optionUI.Init();

        farmCellCount = farmCells.transform.childCount;
        townCellCount = townCells.transform.childCount;
        factoryCellCount = factoryCells.transform.childCount;
    }

    public void AddDestroyedCells(Areas area)
    {
        switch (area)
        {
            case Areas.Farm:
                curFarmCellCount += 1;
                CheckCells(area);
                break;
            case Areas.Town:
                curTownCellCount += 1;
                CheckCells(area);
                break;
            case Areas.Factory:
                curFactoryCellCount += 1;
                CheckCells(area);
                break;
        }
    }

    private void CheckCells(Areas area)
    {
        switch (area)
        {
            case Areas.Farm:
                if (curFarmCellCount == farmCellCount)
                {
                    Spawner[] farmSpawnerList = farmSpawners.GetComponentsInChildren<Spawner>();
                    foreach(Spawner spawner in farmSpawnerList)
                        spawner.SetSpawn(false);
                    SetBoss(area);
                }
                break;
            case Areas.Town:
                if (curTownCellCount == townCellCount)
                {
                    Spawner[] townSpawnerList = townSpawners.GetComponentsInChildren<Spawner>();
                    foreach(Spawner spawner in townSpawnerList)
                        spawner.SetSpawn(false);
                    SetBoss(area);
                }
                break;
            case Areas.Factory:
                if (curFactoryCellCount == factoryCellCount)
                {
                    Spawner[] factorySpawnerList = factorySpawners.GetComponentsInChildren<Spawner>();
                    foreach(Spawner spawner in factorySpawnerList)
                        spawner.SetSpawn(false);
                    SetBoss(area);
                }
                break;
        }
    }

    private void SetBoss(Areas area)
    {
        if (_boss1 == false)
        {
            _boss1 = true;
            _boss1Point = area;
        }
        else if (_boss2 == false)
        {
            _boss2 = true;
            _boss2Point = area;
        }
        else if (_boss3 == false)
        {
            _boss3 = true;
            _boss3Point = area;
        }
    }

    public void CheckBoss()
    {
        if (_boss1 && !_boss1Spawned)
        {
            _boss1Spawned = true;
            cutSceneManager.PlayBossSpawn(1, _boss1Point);
            bossSpawner.SpawnBoss(1, _boss1Point);
        }
        else if (_boss2 && !_boss2Spawned)
        {
            _boss2Spawned = true;
            cutSceneManager.PlayBossSpawn(2, _boss2Point);
            bossSpawner.SpawnBoss(2, _boss1Point);
        }
        else if (_boss3 && !_boss3Spawned)
        {
            _boss3Spawned = true;
            Spawner[] farmSpawnerList = farmSpawners.GetComponentsInChildren<Spawner>();
            foreach(Spawner spawner in farmSpawnerList)
                spawner.SetSpawn(true);
            Spawner[] townSpawnerList = townSpawners.GetComponentsInChildren<Spawner>();
            foreach(Spawner spawner in townSpawnerList)
                spawner.SetSpawn(true);
            Spawner[] factorySpawnerList = factorySpawners.GetComponentsInChildren<Spawner>();
            foreach(Spawner spawner in factorySpawnerList)
                spawner.SetSpawn(true);
            cutSceneManager.PlayBossSpawn(3, _boss3Point);
            bossSpawner.SpawnBoss(3, _boss3Point);
        }
    }

    public void UpgradeAIs(WeaponGrade grade)
    {
        foreach(PlayerAI i in ai)
        {
            i.SetGrade(grade);
        }
    }
    public int GetTotalCells(Areas area)
    {
        switch (area)
        {
            case Areas.Farm: return farmCellCount;
            case Areas.Town: return townCellCount;
            case Areas.Factory: return factoryCellCount;
            default: return 0;
        }
    }

    public int GetDestroyedCells(Areas area)
    {
        switch (area)
        {
            case Areas.Farm: return curFarmCellCount;
            case Areas.Town: return curTownCellCount;
            case Areas.Factory: return curFactoryCellCount;
            default: return 0;
        }
    }

}
