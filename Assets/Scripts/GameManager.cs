using ObjectPooling;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerUnit playerUnit;
    public static Transform playerUnitSpawnPoint;
    
        
    void Awake()
    {
        playerUnitSpawnPoint = FindObjectOfType<PlayerUnitSpawnPoint>().transform;
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }

    private void Start()
    {
        FloatingTextManager.Init();
        StartGame();
    }

    public void StartGame()
    {
        playerUnit = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.PlayerUnit).GetPooledObject().GetComponent<PlayerUnit>();
        playerUnit.transform.position = playerUnitSpawnPoint.position;
        playerUnit.gameObject.SetActive(true);
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameStart, null);
    }
}
