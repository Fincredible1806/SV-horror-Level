using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantZombie : MonoBehaviour
{
   [SerializeField] private RankingSystem rSystem;
    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log(rSystem.name);
    }
    public void AddToRankingSystem()
    {
        rSystem.KillBigZombie();
    }
}
