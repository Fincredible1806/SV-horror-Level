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
    private RankingSystem systemer;
    [SerializeField] int boxFill;
    public EnemyBox box;
    [SerializeField] GameObject partFX;
    ParticleSystem partSys;
    ZombieHolder holder;
    [SerializeField] string holderName;
    [SerializeField] AudioSource source;
    [SerializeField]AudioClip hitClip;
    [SerializeField] AudioClip deadClip;

    [SerializeField] GiantZombie gZombie;

    // Start is called before the first frame update
    void Start()
    {
        gZombie = GetComponent<GiantZombie>();
        if(!isBoxFiller)
        {
            systemer = FindObjectOfType<RankingSystem>();
        }
        source = GetComponent<AudioSource>();
        if (isBoxFiller)
        {
            partSys = partFX.GetComponent<ParticleSystem>();
            partFX.SetActive(true);
            holder = GameObject.Find(holderName).GetComponent<ZombieHolder>();
        }
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        alive = true;

    }

    public void TakeDamage()
    {
        if (alive)
        {
            source.clip = hitClip;
            source.Play();
            Debug.Log("TakeDamage" + damage);
            health -= damage;
            enemyBehaviour.newTargetPosition();
            if (health <= 0)
            {
                KillZombie();
            }
        }
    }

    public void KillZombie()
    {
        if(gZombie != null)
        {
            gZombie.AddToRankingSystem();
        }
        if(systemer != null)
        {
            systemer.zombiesKilled++;
            systemer.PlusOrMinus();
        }
        source.clip = deadClip;
        source.Play();
        if(isBoxFiller && box != null)
        {
            holder.RemoveZombie();
            if(partSys != null)
            {
                partSys.Stop();
            }
            box.chargeRate = boxFill;
            box.SoulFill();
        }
        alive = false;
        animator.SetTrigger(deadTriggername);
        Destroy(gameObject, 3f);
    }
}
