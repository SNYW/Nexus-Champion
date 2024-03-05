using ObjectPooling;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerUnit playerUnit;
    public static Transform playerUnitSpawnPoint;

    [SerializeField] private ObjectPool playerUnitPool;
    [SerializeField] private ObjectPool floatingTextPool;

    void Awake()
    {
        playerUnitSpawnPoint = FindObjectOfType<PlayerUnitSpawnPoint>().transform;
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }

    private void Start()
    {
        FloatingTextManager.Init(floatingTextPool);
        StartGame();
    }

    public void StartGame()
    {
        playerUnit = playerUnitPool.GetPooledObject().GetComponent<PlayerUnit>();
        playerUnit.transform.position = playerUnitSpawnPoint.position;
        playerUnit.gameObject.SetActive(true);
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameStart, null);
    }
}
