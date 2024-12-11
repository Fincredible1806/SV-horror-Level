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
    private EnemyBehaviour enemyBehaviour;
    public bool isBoxFiller = false;
    [SerializeField] int boxFill;
    public EnemyBox box;
    [SerializeField] GameObject partFX;

    // Start is called before the first frame update
    void Start()
    {
        if (isBoxFiller)
        {
            partFX.SetActive(true);
        }
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        alive = true;

    }

    public void TakeDamage()
    {
        if (alive)
        {
            Debug.Log("TakeDamage" + damage);
            health = health - damage;
            enemyBehaviour.newTargetPosition();
            if (health <= 0)
            {
                KillZombie();
            }
        }
    }

    private void KillZombie()
    {
        if(isBoxFiller && box != null)
        {
            box.chargeRate = boxFill;
            box.SoulFill();
        }
        alive = false;
        animator.SetTrigger(deadTriggername);
        Destroy(gameObject, 3f);
    }
}
