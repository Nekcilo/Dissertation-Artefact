using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameObject ScoreScreen;

    //UI Text
    [Header("Score Values")]
    [SerializeField] TMP_Text GoodScore;
    [SerializeField] TMP_Text BadScore;
    [SerializeField] TMP_Text TotalScore;

    [Header("Drink Count Values")]
    [SerializeField] TMP_Text BonusCount;
    [SerializeField] TMP_Text GoodCount;
    [SerializeField] TMP_Text BadCount;

    public void SetUI(float GoodScoreValue, float BadScoreValue, int BonusOrders, int GoodOrders, int BadOrders, float TotalScoreValue)
    {
        GoodScore.text = "Good Drinks: £" + GoodScoreValue.ToString("0.00");
        BadScore.text = "Bad Drinks: £" + BadScoreValue.ToString("0.00");

        BonusCount.text = "Bonus Drinks: " + BonusOrders;
        GoodCount.text = "Good Drinks: " + GoodOrders;
        BadCount.text = "Bad Drinks: " + BadOrders;

        TotalScore.text = "Total: £" + TotalScoreValue.ToString("0.00");
    }

    public void ShowUI()
    {
        ScoreScreen.SetActive(true);
    }

    public void HideUI()
    {
        ScoreScreen.SetActive(false);
    }
}
