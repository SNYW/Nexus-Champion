using UnityEngine;

namespace ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour
    {
        public ObjectPool.ObjectPoolName objectPoolName;

        protected void ReQueue()
        {
            ObjectPoolManager.GetPool(objectPoolName).ReQueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}