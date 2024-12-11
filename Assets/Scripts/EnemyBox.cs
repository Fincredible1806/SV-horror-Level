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
    // Start is called before the first frame update
    void Start()
    {
        enemyBox = GetComponent<EnemyBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoulFill()
    {
        if (!filled)
        {
            zombiesKilled += chargeRate;
            if(zombiesKilled >= zombiesToKill ) 
            {
                ChestFilled();
            }

        }

    }

    private void ChestFilled()
    {
        filled = true;
        filledEvent.Invoke();
        foreach (TimedEnemySpawner spawner in spawns)
        {
            spawner.active = false;
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
            EnemyHealth healthToSet = other.GetComponent<EnemyHealth>();
            healthToSet.isBoxFiller = true;
            healthToSet.box = enemyBox;
        }
    }
}
