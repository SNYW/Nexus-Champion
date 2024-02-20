using System.Collections;
using ObjectPooling;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool.ObjectPoolName poolToSpawn;
    public Transform spawnAnchor;
    public float spawnRadius;

    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(spawnDelay);
            // Generate a random point within the circle
            Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;

            // Instantiate the prefab at the random point
            Vector3 randomPosition =  spawnAnchor.transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

            var newSpawn = ObjectPoolManager.GetPool(poolToSpawn).GetPooledObject();
            newSpawn.transform.position = randomPosition;
            newSpawn.transform.rotation = spawnAnchor.rotation;
            newSpawn.gameObject.SetActive(true);
        }
    }
}
