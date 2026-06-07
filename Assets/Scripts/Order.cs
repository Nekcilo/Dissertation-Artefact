using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [Header("Drink Options")]
    public List<DrinkDefiniton> DrinkOrder = new List<DrinkDefiniton>();
    private DrinkDefiniton Definition;

    //Selection Arrays
    [HideInInspector] public string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    [HideInInspector] public string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //Object References
    //Text References
    [Header("Text References")]
    public TMP_Text DebugText1; //Vessel
    public TMP_Text DebugText2; //Ingredient
    public TMP_Text DebugText3; //Liquid

    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;

    //SpriteRenderer Reference
    [Header("Sprite Renderer References")]
    [SerializeField] public SpriteRenderer PourLiquid;
    [SerializeField] public SpriteRenderer CupLiquid;

    //Animator References
    [Header("Animator References")]
    public Animator CupAnimator;
    public Animator PourAnimator;
    public Animator FeedbackAnimator;

    //Script References
    [Header("Script References")]
    [SerializeField] Hardware HardwareScript;
    [SerializeField] Score ScoreScript;
    [SerializeField] GameTimer TimerScript;

    //Round Started Boolean
    [HideInInspector] public bool RoundStarted;

    public int RandomInt()
    {
        return Random.Range(0, DrinkOrder.Count); //The maximum parameter is exclusive
    }

    public void CustomerOrder(int CustOrder)
    {
        if (!TimerScript.GameActive)
        {
            RoundStarted = false;
        }
        else if (TimerScript.GameActive)
        {
            RoundStarted = true;
            
            if (RoundStarted)
            {
                Definition = DrinkOrder[CustOrder];

                HardwareScript.RequiredVessel = Definition.RequiredVessel;
                HardwareScript.RequiredIngredient = Definition.RequiredIngredient;
                HardwareScript.RequiredLiquid = Definition.RequiredLiquid;

                if (HardwareScript.PreviousVessel == HardwareScript.RequiredVessel || HardwareScript.PreviousIngredient == HardwareScript.RequiredIngredient || HardwareScript.PreviousLiquid == HardwareScript.RequiredLiquid)
                {
                    CustomerOrder(RandomInt());
                }
                else
                {
                    DisplayOrder(Definition.DrinkName, HardwareScript.RequiredLiquid);
                    Debug.Log("Round Started");
                }
            }
        }
    }

    void DisplayOrder(string CustOrder, string Liquid)
    {
        OrderLine1.text = CustOrder;

        if (Liquid == LiquidSelection[1])
        {
            OrderLine2.text = "With Water";
        }
        else if (Liquid == LiquidSelection[2])
        {
            OrderLine2.text = "With Cows Milk";
        }
        else if (Liquid == LiquidSelection[3])
        {
            OrderLine2.text = "With Oat Milk";
        }

    }

    public void DrinkCheck()
    {
        if (TimerScript.GameActive)
        {
            if (HardwareScript.SelectedVessel == HardwareScript.RequiredVessel && HardwareScript.SelectedIngredient == HardwareScript.RequiredIngredient && HardwareScript.SelectedLiquid == HardwareScript.RequiredLiquid)
            {
                //FeedbackAnimator.SetBool("Neg", false);
                FeedbackAnimator.SetBool("Pos", true);

                if (ScoreScript.FastPreparation())
                {
                    ScoreScript.BonusOrders += 1;
                    Debug.Log("BONUS! Score +3.2");
                }
                else if (!ScoreScript.FastPreparation())
                {
                    ScoreScript.GoodOrders += 1;
                    Debug.Log("Score +1.6");
                }
            }
            else
            {
                //FeedbackAnimator.SetBool("Pos", false);
                FeedbackAnimator.SetBool("Neg", true);

                ScoreScript.BadOrders += 1;

                Debug.Log("Score -1.4");
            }

            HardwareScript.PreviousVessel = HardwareScript.RequiredVessel;
            HardwareScript.PreviousIngredient = HardwareScript.RequiredIngredient;
            HardwareScript.PreviousLiquid = HardwareScript.RequiredLiquid;

            HardwareScript.ResetRound();
        }
    }
}
