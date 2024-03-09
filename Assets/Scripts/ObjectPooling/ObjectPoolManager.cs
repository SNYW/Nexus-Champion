using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPooling
{
    public static class ObjectPoolManager
    {
        private static List<ObjectPool> _pools;
        public static Transform _pooledObjectAnchor;

        public static void InitPools()
        {
            _pooledObjectAnchor = GameObject.Find("Pooled Objects").transform;
            var allPools = Resources.LoadAll("Data/Pools", typeof(ObjectPool)).Cast<ObjectPool>().ToList();

            _pools = allPools;
        
            foreach (var objectPool in _pools)
            {
                objectPool.InitPool();
            }
        }
    }
}
