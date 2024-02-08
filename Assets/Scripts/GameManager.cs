using System.Collections;
using System.Collections.Generic;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
