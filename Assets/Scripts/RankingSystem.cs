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
    [SerializeField] private float rankToCheck;
    [SerializeField] private int currentRanking;
    [SerializeField] private TextMeshProUGUI currentRankText;
    [SerializeField] private TextMeshProUGUI currentPlusOrMinus;
    private int zombiesInMap;
    [SerializeField] public int zombiesKilled;
    // Start is called before the first frame update
    void Start()
    {
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
        currentRanking++;
        currentRankText.text = "Current Rank: " + rankNames[currentRanking];
    }

    private void PlusOrMinus()
    {
        if(zombiesKilled >= zombiesInMap)
        {
            currentPlusOrMinus.text = plusAndMinus[1];
        }
    }
}
