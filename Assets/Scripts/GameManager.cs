using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }
}
