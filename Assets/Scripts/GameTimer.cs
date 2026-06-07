using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Game Booleans")]
    public bool GameActive;
    public bool HasCalculated;
    public bool Replay = false;
    public bool CooldownEnded;

    //Timer Values
    [Header("Elapsed Time")]
    public float ElapsedTime = 0f;
    float TimerLength = 60f;

    public float CooldownTime = 0f;
    float CooldownLength = 5f;

    //Script References
    [Header("Script References")]
    [SerializeField] Score ScoreScript;
    [SerializeField] ScoreUI ScoreUIScript;
    [SerializeField] Hardware HardwareScript;

    private void Awake()
    {
        ScoreUIScript.HideUI();

        GameActive = true;

        HardwareScript.ResetRound();
        HardwareScript.ResetAnim();
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
            Replay = false;
        }
    }

    public void ResetGame()
    {
        if (!GameActive)
        {
            CooldownTime += Time.deltaTime;

            if (!HasCalculated)
            {
                Debug.Log("Game Finished");

                //HardwareScript.ResetRound();

                HasCalculated = ScoreScript.TotalScoreCalculator();

                ScoreUIScript.ShowUI();
            } 

            if (CooldownTimer() && Replay)
            {
                ScoreUIScript.HideUI();

                ScoreScript.ResetScore();
                GameActive = true;
                ElapsedTime = 0;

                HardwareScript.ResetRound();
            }
        }
    }

    bool CooldownTimer()
    {
        if (CooldownTime >= CooldownLength)
        {
            //Debug.Log("Cooldown Ended");
            return true;
        }

        return false;
    }
}
