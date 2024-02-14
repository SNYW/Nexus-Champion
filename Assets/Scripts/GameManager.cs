using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject playerUnit;
    
        
    void Awake()
    {
        playerUnit = GameObject.Find("Player Unit");
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }
}
