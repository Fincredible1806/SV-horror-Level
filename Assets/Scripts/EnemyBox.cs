using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Events;
public class EnemyBox : MonoBehaviour
{
    [SerializeField] private int zombiesToKill;
    [SerializeField] private int zombiesKilled; 
    public int chargeRate;
    [SerializeField] private UnityEvent filledEvent;
    private bool filled;
    [SerializeField] string enemyTag;
    public EnemyBox enemyBox;
    [SerializeField] List<TimedEnemySpawner> spawns;
    public ParticleSystem FXsystem;
    private bool doubleSpeed;
    private bool extraSpeed;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        enemyBox = GetComponent<EnemyBox>();
    }

    public void SoulFill()
    {
        if (!filled)
        {
            zombiesKilled += chargeRate;
            FXsystem.Play();
            source.Play();
            if(zombiesKilled >= zombiesToKill ) 
            {
                ChestFilled();
            }
            if(zombiesKilled >= (zombiesToKill / 2) && !doubleSpeed)
            {
                doubleSpeed = true; 
                foreach (var spawner in spawns)
                {
                    spawner.timeBetweenSpawns /= 2;
                }
            }
            if(zombiesKilled >= (zombiesToKill * 0.75) && !extraSpeed)
            {
                extraSpeed = true;
                foreach (var spawner in spawns)
                {
                    spawner.timeBetweenSpawns /= 2;
                }
            }
        }

    }

    private void ChestFilled()
    {
        filled = true;
        filledEvent.Invoke();
        FXsystem.transform.localScale = FXsystem.transform.localScale * 2;
        FXsystem.Play();
        foreach (TimedEnemySpawner spawner in spawns)
        {
            spawner.DeActivate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
           EnemyHealth healthToSet = other.GetComponent<EnemyHealth>();
            healthToSet.isBoxFiller = true;
            healthToSet.box = enemyBox;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            if(!filled)
            {
                EnemyHealth healthToSet = other.GetComponent<EnemyHealth>();
                healthToSet.isBoxFiller = true;
                healthToSet.box = enemyBox;
            }
            if(filled)
            {
                other.GetComponent<EnemyHealth>().KillZombie();
            }

        }
    }
}
