using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using UnityEngine;

public static class ObjectPoolManager
{
    private static List<ObjectPool> _pools;

    public static void InitPools()
    {
        var allPools = Resources.LoadAll("Data/Pools", typeof(ObjectPool)).Cast<ObjectPool>().ToList();

        _pools = allPools;
        
        foreach (var objectPool in _pools)
        {
          objectPool.InitPool();
        }
    }
}
