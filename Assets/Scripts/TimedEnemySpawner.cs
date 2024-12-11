using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TimedEnemySpawner : MonoBehaviour
{
    [SerializeField] float xRange;
    [SerializeField] float yRange;
    Vector3 spawnRange;
    public int enemiesToSpawn;
    private int enemiesSpawned;
    public List<GameObject> enemies;
    public bool active = false;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float timePassed;
    [SerializeField] GameObject spawnFX;
    // Start is called before the first frame update
    void Start()
    {
        spawnRange = new Vector3(xRange, yRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeBetweenSpawns)
            {
                SpawnEnemies();
                timePassed = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange, 0, yRange));
    }

    public void SpawnEnemies()
    {
            int spawnedEnemy = Random.Range(0, enemies.Count);
            GameObject enemyToSpawn = enemies[spawnedEnemy];
            Vector3 samplePos = transform.position + new Vector3((Random.Range(-xRange, xRange)), 0, Random.Range(-yRange, yRange));
            NavMesh.SamplePosition(samplePos, out NavMeshHit hit, 100, 0);
            Instantiate(enemyToSpawn, samplePos, Quaternion.identity);
    }

    public void Activate()
    {
        active = true;
        spawnFX.SetActive(true);
    }

    public void DeActivate()
    {
        active = false;
        spawnFX.SetActive(false);
    }
}
