using UnityEngine;

namespace ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour
    {
        public ObjectPool pool;

        protected void ReQueue()
        { 
            transform.position = Vector3.zero;
            transform.parent = ObjectPoolManager._pooledObjectAnchor;
            pool.ReQueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}