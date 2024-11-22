using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float xRange;
    [SerializeField] float yRange;
    Vector3 spawnRange;
    public int enemiesToSpawn;
    private int enemiesSpawned;
    [SerializeField] GameObject spawnedObject;
    // Start is called before the first frame update
    void Start()
    {
        enemiesSpawned = 0;
        spawnRange = new Vector3(xRange, yRange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange, 0, yRange));
    }

    public void SpawnEnemies()
    {
        while (enemiesSpawned < enemiesToSpawn)
        {
            Vector3 samplePos = transform.position + new Vector3((Random.Range(-xRange, xRange)), 0, Random.Range(-yRange, yRange));
            NavMesh.SamplePosition(samplePos, out NavMeshHit hit, 100, 0);
            Instantiate(spawnedObject, samplePos, Quaternion.identity);
            enemiesSpawned++;
        }
        if (enemiesSpawned > enemiesToSpawn)
        {
            enemiesSpawned = 0;
        }
    }
}
