using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
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
            
            Instantiate(enemyToSpawn, randomPosition, spawnAnchor.rotation);
        }
    }
}
