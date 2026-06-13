using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    //Score Values
    [HideInInspector] public float GoodScoreValue;
    [HideInInspector] public float BadScoreValue;
    [HideInInspector] public float TotalScoreValue = 0f;

    //Order Counts
    [Header("Order Counts")]
    public int BonusOrders = 0;
    public int GoodOrders = 0;
    public int BadOrders = 0;

    [HideInInspector] public float PreviousTime = 0f;
    
    float BonusTime = 15f;

    //Script References
    [Header("Script References")]
    [SerializeField] Order OrderScript;
    [SerializeField] ScoreUI ScoreUIScript;

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
        bool Outcome;

        if (PreviousTime < BonusTime)
        {
            Outcome = true;
        }
        else
        {
            Outcome = false;
        }

        return Outcome;
    }

    public bool TotalScoreCalculator()
    {
        //Good + Bad Scores £
        GoodScoreValue += ((GoodOrders * 1.60f) + (BonusOrders * 3.20f));
        BadScoreValue += (BadOrders * 1.40f);

        ////Total Score
        TotalScoreValue = GoodScoreValue - BadScoreValue;

        ScoreUIScript.SetUI(GoodScoreValue, BadScoreValue, BonusOrders, GoodOrders, BadOrders, TotalScoreValue);

        return true; //Return true for HasCalculated in GameTimer
    }

    public void ResetScore()
    {
        GoodScoreValue = 0f;
        BadScoreValue = 0f;
        TotalScoreValue = 0f;
        
        BonusOrders = 0;
        GoodOrders = 0;
        BadOrders = 0;
    }
}
