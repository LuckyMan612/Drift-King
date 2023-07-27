using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomSpawner : MonoBehaviour
{

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float spawnRadius = 100f;
    [SerializeField] float spawnInterval = 6f;
    [SerializeField] float yOffset = 0.5f;
    [SerializeField] float gasOffset = 0.5f;
    [SerializeField] private GameObject objectSpawned;

    [SerializeField] private bool onDelay = false;
    [SerializeField] private bool isGas;

    void Update()
    {
        if(onDelay)
            return;

        SpawnObject();
    }

    void SpawnObject()
    {
        //Generate a random position within the spawn radius
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition = new Vector3(randomPosition.x, yOffset, randomPosition.z);
        randomPosition += transform.position;

        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            //Spawn the object and set the delay

            randomPosition = hit.position;

            if (isGas)
            {
                randomPosition = new Vector3(randomPosition.x, randomPosition.y + gasOffset, randomPosition.z);
            }

            objectSpawned = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);


            onDelay = true;
            Invoke("ResetDelay", spawnInterval);
        }
    }

    void ResetDelay() => onDelay = false;

    void OnDrawGizmosSelected()
    {
        // Draw the spawn radius
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
