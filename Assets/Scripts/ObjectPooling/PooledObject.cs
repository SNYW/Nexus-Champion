using UnityEngine;

namespace ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour
    {
        public ObjectPool pool;

        protected void ReQueue()
        {
            pool.ReQueue(gameObject);
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}