using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private float[] rankTimes;
    [SerializeField] private string[] rankNames;
    [SerializeField] private string[] plusAndMinus;
    [SerializeField] private EnemyHealth[] allZombies;
    [Header("TextMeshPros")]
    [SerializeField] private TextMeshProUGUI finalRankText;
    [SerializeField] private TextMeshProUGUI bigGameHunter;
    [SerializeField] TextMeshProUGUI zombiesToPlus;
    [SerializeField] private TextMeshProUGUI currentRankText;
    [SerializeField] private TextMeshProUGUI currentPlusOrMinus;
    [SerializeField] private TextMeshProUGUI totalZombiesKilledText;
    [Header("Timings")]
    [SerializeField] private float timeTaken = 0;
    [SerializeField] private float rankToCheck;
    [SerializeField] private int currentRanking;
    [Header("Zombie Stuff")]
    public int zombiesInMap;
    public int zombiesKilled;
    [SerializeField] private int bigZombies;
    public int bigZombiesKilled;
    int finalTiming;
    bool isPlus;

    // Start is called before the first frame update
    void Start()
    {
        isPlus = false;    
        rankToCheck = rankTimes[0];
        currentRankText.text = "Current Rank: " + rankNames[currentRanking];
        currentRanking = 1;
        currentPlusOrMinus.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
        if (timeTaken > rankToCheck)
        {
            RankChanger();
        }

    }

    private void RankChanger()
    {
        rankToCheck = rankTimes[currentRanking];
        currentRankText.text = "Current Rank: " + rankNames[currentRanking];
        currentRanking++;
    }

    public void PlusOrMinus()
    {
        if(zombiesKilled >= zombiesInMap)
        {
            isPlus = true;
            currentPlusOrMinus.text = plusAndMinus[1];
        }
    }

    public void WinGame()
    {
        allZombies = FindObjectsOfType<EnemyHealth>();
        StartCoroutine(FinalStuff());
    }

    IEnumerator FinalStuff()
    {
        foreach(EnemyHealth health in allZombies)
        {
            Destroy(health.gameObject);
        }
        yield return new WaitForSeconds(1);
        if (!isPlus)
        {
            Debug.Log("Isnt plus");
            zombiesToPlus.gameObject.SetActive(true);
            zombiesToPlus.text = "Zombies needed to earn the SLAYER accolade: " + (zombiesInMap - zombiesKilled);
        }
        totalZombiesKilledText.text = "Total Zombies Killed: " + zombiesKilled;
        if (bigZombiesKilled >= bigZombies)
        {
            bigGameHunter.gameObject.SetActive(true);
        }
        int finalRankValue;
        finalRankValue = Mathf.Clamp(currentRanking - 1, 0, rankNames.Length);
        Debug.Log(finalRankValue);
        finalTiming = (int)timeTaken;
        finalRankText.text = "Time Taken: " + finalTiming + " seconds ," + " Final Rank: " + rankNames[finalRankValue] + " " + currentPlusOrMinus.text;
    }

    public void KillBigZombie()
    {
        bigZombiesKilled++;
    }


}
