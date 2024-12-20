using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHolder : MonoBehaviour
{
    public float currentSpawnedZombies = 0;
    public float maxZombies;

    public void AddZombie()
    {
        currentSpawnedZombies++;
    }

    public void RemoveZombie()
    {
        currentSpawnedZombies--;
    }
}
