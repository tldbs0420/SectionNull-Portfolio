using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public PlayerMovement playerMovement;


    [Header("Managers")]
    public PoolManager poolManager;
    public DayTest dayTest;
    public EventBus eventBus;

    [Header("Boss")]
    [SerializeField] Boss1 boss1;
    [SerializeField] Boss2 boss2;
    [SerializeField] Boss3 boss3;


    void Awake()
    {
        InitGame();
    }

    public void InitGame()
    {
        if (playerMovement)
            playerMovement.Init();
        if (eventBus)
            eventBus.Init();
        if (poolManager)
            poolManager.Init();
        if (dayTest)
            dayTest.Init();
        if (boss1)
            boss1.Init(boss1.waypoint, boss1.waypoints);
        if (boss2)
            boss2.Init(boss2.waypoint, boss2.waypoints);
        if (boss3)
            boss3.Init(boss3.waypoint, boss3.waypoints);
        
    }
}
