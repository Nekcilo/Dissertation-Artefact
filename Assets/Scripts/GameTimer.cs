using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public bool GameActive;
    [HideInInspector] public bool HasCalculated;
    [HideInInspector] public bool Replay = false;
    [HideInInspector] public int Rounds = 0;

    //Timer Values
    [Header("Elapsed Time")]
    public float ElapsedTime = 0f;
    float TimerLength = 60f;

    //Cooldown Variables
    [Header("Cooldown Timer")]
    public float CooldownTime = 0f;
    float CooldownLength = 5f;
    public bool CooldownEnded;

    //Visual Timer
    [Header("Visual Timer")]
    [SerializeField] RectTransform TimerBar;

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

        Debug.Log("TimerBar Size: " + TimerBar.sizeDelta);
    }

    private void Update()
    {
        if (ElapsedTime >= TimerLength)
        {
            GameActive = false;
            ResetGame();
        }
        else if (ElapsedTime <= TimerLength && Rounds > 1)
        {
            ElapseTime();
            VisualTimer();
        }
    }

    public void ElapseTime()
    {
        if (GameActive)
        {
            ElapsedTime += Time.deltaTime;
            Replay = false;
        }
    }

    void VisualTimer()
    {
        TimerBar.sizeDelta = new Vector2(((1920 - (ElapsedTime / TimerLength) * 1920)), TimerBar.sizeDelta.y);
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
                Rounds = 0;

                HardwareScript.ResetRound();
            }
        }
    }

    bool CooldownTimer()
    {
        if (CooldownTime >= CooldownLength)
        {
            return true;
        }

        return false;
    }
}
