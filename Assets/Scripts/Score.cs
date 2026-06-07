using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float GoodScoreValue;
    public float BadScoreValue;
    public float TotalScoreValue = 0f;

    public int BonusOrders = 0;
    public int GoodOrders = 0;
    public int BadOrders = 0;
    
    public float PreviousTime = 0f;
    
    float BonusTime = 10f;

    bool Outcome;

    [SerializeField] Order OrderScript;

    private void Update()
    {
        //Bonus Score Timer
        if (OrderScript.RoundStarted)
        {
            PreviousTime += Time.deltaTime;
        }
        else if (!OrderScript.RoundStarted)
        {
            PreviousTime = 0f;
        }
    }

    public bool FastPreparation()
    {
        if (PreviousTime > BonusTime)
        {
            Outcome = false;
        }
        else if (PreviousTime < BonusTime)
        {
            Outcome = true;
        }

        return Outcome;

    }

    public bool TotalScoreCalculator()
    {
        //Good + Bad Scores £
        GoodScoreValue += ((GoodOrders * 1.60f) + (BonusOrders * 3.20f));
        BadScoreValue -= (BadOrders * 1.40f);
        Debug.Log("Good Drinks: £" + GoodScoreValue);
        Debug.Log("Bad Drinks: £" + BadScoreValue);


        //Individual Bonus + Good Order Counts
        Debug.Log("Bonus Drinks: " + BonusOrders);
        Debug.Log("Good Drinks: " + GoodOrders);


        //Total Score
        TotalScoreValue = GoodScoreValue + BadScoreValue;
        Debug.Log("Total: £" + TotalScoreValue);

        return true; //Return true for HasCalculated in GameTimer
    }

    public void ResetScore()
    {
        TotalScoreValue = 0f;
        BonusOrders = 0;
        GoodOrders = 0;
        BadOrders = 0;
    }
}
