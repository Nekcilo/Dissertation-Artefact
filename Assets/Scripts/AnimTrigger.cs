using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    //Script References
    [Header("Script References")]
    [SerializeField] Order OrderScript;
    [SerializeField] Hardware HardwareScript;
    [SerializeField] VisualDrink VisualDrinkScript;
    public void EntryPourAnim()
    {
        VisualDrinkScript.DrinkPouring = true;
    }

    public void DrinkFull()
    {
        HardwareScript.drinkFull = true;
        OrderScript.PourAnimator.SetBool("AnimIsPouring", false);
    }

    public void ExitPourAnim()
    {
        HardwareScript.ResetAnim();
    }
}
