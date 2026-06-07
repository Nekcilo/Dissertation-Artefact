using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public bool GameActive;
    public bool HasCalculated;

    float TimerLength = 60f;
    public float ElapsedTime = 0f;

    [SerializeField] Score ScoreScript;
    [SerializeField] Hardware HardwareScript;

    private void Awake()
    {
        GameActive = true;

        HardwareScript.ResetRound();
    }

    private void Update()
    {
        if (ElapsedTime >= TimerLength)
        {
            GameActive = false;
            ResetGame();
        }
        else if (ElapsedTime <= TimerLength)
        {
            ElapseGame();
        }
    }

    public void ElapseGame()
    {
        if (GameActive)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void ResetGame()
    {
        if (!GameActive && !HasCalculated)
        {
            Debug.Log("Game Finished");

            HardwareScript.ResetRound();

            HasCalculated = ScoreScript.TotalScoreCalculator();
        }

        //if (//Button Clicked//)
        //{
        //    ScoreScript.ResetScore();
        //    GameActive = true;
              //ElapsedTime = 0;
        //}
    }
}
