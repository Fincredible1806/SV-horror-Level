using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool alive = true;
    public int health;
    public int damage = 25;
    [SerializeField] Animator animator;
    [SerializeField] private string deadTriggername;
    // Start is called before the first frame update
    void Start()
    {
        alive = true;
    }

    public void TakeDamage()
    {
        Debug.Log("TakeDamage" + damage);
        health = health -damage;
        if (health <= 0)
        {
            KillZombie();
        }
    }

    private void KillZombie()
    {
        alive = false;
        animator.SetTrigger(deadTriggername);
        Destroy(gameObject, 3f);
    }
}
