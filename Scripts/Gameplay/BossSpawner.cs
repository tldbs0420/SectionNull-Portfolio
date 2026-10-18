using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class BossSpawner : MonoBehaviour
{
    [Header("BOSS")]
    [SerializeField] GameObject boss1;
    [SerializeField] GameObject boss2;
    [SerializeField] GameObject boss3;

    [Header("SpawnPoint")]
    [SerializeField] Transform boss1Point;
    [SerializeField] Transform boss2Point;
    [SerializeField] Transform boss3Point;
    [Header("WayPoint")]
    [SerializeField] Transform wayPoint;
    [SerializeField] List<GameObject> boss1Waypoints;
    [SerializeField] List<GameObject> boss2Waypoints;
    [SerializeField] List<GameObject> boss3Waypoints;


    public void SpawnBoss(int bossNum, GameManager.Areas area)
    {
        Enemy enemy;
        Transform bossPoint = GetSpawnPoint(area);
        List<GameObject> waypoints = GetWaypoints(area);

        switch (bossNum)
        {
            case 1:
                boss1.transform.position = bossPoint.position;
                boss1.SetActive(true);
                enemy = boss1.GetComponent<Boss1>();
                break;
            case 2:
                boss2.transform.position = bossPoint.position;
                boss2.SetActive(true);
                enemy = boss2.GetComponent<Boss2>();
                break;
            case 3:
                boss3.transform.position = bossPoint.position;
                boss3.SetActive(true);
                enemy = boss3.GetComponent<Boss3>();
                break;
            default:
                return;
        }

        if (enemy) enemy.Init(wayPoint, waypoints);
    }

    private Transform GetSpawnPoint(GameManager.Areas area)
    {
        switch (area)
        {
            case GameManager.Areas.Farm:
                return boss1Point;
            case GameManager.Areas.Town:
                return boss2Point;
            case GameManager.Areas.Factory:
                return boss3Point;
            default:
                return null;
        }
    }

    private List<GameObject> GetWaypoints(GameManager.Areas area)
    {
        switch (area)
        {
            case GameManager.Areas.Farm:
                return boss1Waypoints;
            case GameManager.Areas.Town:
                return boss2Waypoints;
            case GameManager.Areas.Factory:
                return boss3Waypoints;
            default:
                return null;
        }
    }
}
