using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Pouring : MonoBehaviour
{
    [SerializeField] Transform PourOrigin;
    [SerializeField] GameObject LiquidPouring;
    [SerializeField] GameObject LiquidInCup;

    [SerializeField] public Animator CupAnimator;
    [SerializeField] public Animator PourAnimator;

    [SerializeField] GameObject Liquid;

    bool isCupFull;
    Vector3 DefaultScale;
    Vector3 DefaultPos;

    public int AnimCheck;

    private void Awake()
    {
        DefaultScale = LiquidInCup.transform.localScale;
        DefaultPos = LiquidInCup.transform.position;
    }

    public void PourCheck(bool IsPouring)
    {
        if (IsPouring)
        {

            PourAnimator.SetBool("AnimIsPouring", true);

            isCupFull = FullCupCheck();

            if(isCupFull)
            {
                IsPouring = false;
            }    
        }
        else
        {

        }
    }

    private bool FullCupCheck()
    {
        if (LiquidInCup.transform.localScale == DefaultScale && LiquidInCup.transform.position == DefaultPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
