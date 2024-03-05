using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(fileName = "new Pool", menuName = "Game Data/Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        [SerializeField] private GameObject pooledObject;
        [SerializeField] private int minAmount;

        private Transform _pooledObjectParent;

        private Queue<GameObject> _pool;

        public GameObject GetPooledObject()
        {
            if (_pool.TryDequeue(out var pooledItem))
            {
                return pooledItem;
            }

            var newPooledObject = Instantiate(pooledObject, Vector2.zero, Quaternion.identity, _pooledObjectParent);
            newPooledObject.SetActive(false);
            return newPooledObject;
        }

        public void InitPool()
        {
            _pooledObjectParent = GameObject.Find("Pooled Objects").transform;
            
            if (_pool != null)
            {
               ClearPool();
            }
            else
            {
                _pool = new Queue<GameObject>();
            }
            
            for (int i = 0; i < minAmount; i++)
            {
                var newPooledObject = Instantiate(
                    pooledObject, 
                    Vector2.zero,
                    Quaternion.identity, 
                    _pooledObjectParent
                );
                
                newPooledObject.SetActive(false);
                _pool.Enqueue(newPooledObject);     
            }
        }

        public void ReQueue(GameObject obj)
        {
            _pool.Enqueue(obj);
        }

        public List<GameObject> GetAllActive()
        {
            var returnPool = new List<GameObject>();

            foreach (var o in _pool)
            {   
                if(o.activeSelf)
                    returnPool.Add(o);
            }

            return returnPool;
        }

        private void ClearPool()
        {
            while (_pool.Count > 0)
            {
                GameObject obj = _pool.Dequeue();
                Destroy(obj);
            }
            
            _pool.Clear();
        }
        
    }
}
