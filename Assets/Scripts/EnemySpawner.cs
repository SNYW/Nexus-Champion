using System;
using System.Collections;
using ObjectPooling;
using SystemEvents;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool enemyPool;
    public Transform spawnAnchor;
    public float spawnRadius;

    public float spawnDelay;

    private void OnEnable()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameStart, Enable);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameEnd, Disable);
    }

    private void Enable(object obj)
    {
        StartCoroutine(Spawn());
    }

    private void Disable(object obj)
    {
        StopAllCoroutines();
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

            var newSpawn = enemyPool.GetPooledObject();
            newSpawn.transform.position = randomPosition;
            newSpawn.transform.rotation = spawnAnchor.rotation;
            newSpawn.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameStart, Enable);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameEnd, Disable);
    }
}
