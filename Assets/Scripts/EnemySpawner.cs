using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public Transform spawnAnchor;

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
            Instantiate(enemyToSpawn, spawnAnchor.transform.position, spawnAnchor.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
