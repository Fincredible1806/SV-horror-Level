using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    [SerializeField] private float[] rankTimes;
    [SerializeField] private string[] rankNames;
    [SerializeField] private string[] plusAndMinus;
    [SerializeField] private float timeTaken = 0;
    bool isPlus;
    [SerializeField] private float rankToCheck;
    [SerializeField] private int currentRanking;
    [SerializeField] private TextMeshProUGUI currentRankText;
    [SerializeField] private TextMeshProUGUI currentPlusOrMinus;
    private int zombiesInMap;
    public int zombiesKilled;
    [SerializeField] private TextMeshProUGUI finalRankText;
    int finalTiming;
    [SerializeField] TextMeshProUGUI zombiesToPlus;
    // Start is called before the first frame update
    void Start()
    {
        isPlus = false;    
        zombiesInMap = GameObject.FindObjectsOfType(typeof(EnemyBehaviour)).Length; ;
        rankToCheck = rankTimes[0];
        currentRankText.text = "Current Rank: " + rankNames[currentRanking];
        currentRanking = 1;
        currentPlusOrMinus.text = plusAndMinus[0];
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
        if(zombiesKilled >= zombiesInMap/2)
        {
            isPlus = true;
            currentPlusOrMinus.text = plusAndMinus[1];
        }
    }

    public void WinGame()
    {
        if(!isPlus)
        {
            zombiesToPlus.text = "Zombies needed to get a plus: " + (zombiesInMap - zombiesKilled);
        }
        int finalRankValue;
        finalRankValue = Mathf.Clamp(currentRanking-1,0, rankNames.Length);
        Debug.Log(finalRankValue);
        finalTiming = (int)timeTaken;
        finalRankText.text = "Time Taken: " + finalTiming + " seconds ," + " Final Rank: " + rankNames[finalRankValue] + " " + currentPlusOrMinus.text;
    }
}
