using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] Order orderScript;

    public void EntryPourAnim()
    {
        orderScript.CupAnimator.SetBool("AnimIsPouring", true);
    }

    public void DrinkFull()
    {
        orderScript.drinkFull = true;
        orderScript.PourAnimator.SetBool("AnimIsPouring", false);
    }
    public void ExitPourAnim()
    {
        orderScript.Reset();
    }
}
