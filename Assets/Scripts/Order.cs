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
    public string[] VesselSelection = { "None", "Mug", "Cup"};
    [HideInInspector] public string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    [HideInInspector] public string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //Object References
    //Text References
    [Header("Text References")]
    public TextMeshProUGUI DebugText1; //Vessel
    public TextMeshProUGUI DebugText2; //Ingredient
    public TextMeshProUGUI DebugText3; //Liquid

    [SerializeField] TextMeshProUGUI OrderLine1;
    [SerializeField] TextMeshProUGUI OrderLine2;

    //SpriteRenderer Reference
    [Header("Sprite Renderer References")]
    [SerializeField] public SpriteRenderer PourLiquid;

    //Animator References
    [Header("Animator References")]
    public Animator PourAnimator;
    public Animator FeedbackAnimator;

    //Script References
    [Header("Script References")]
    [SerializeField] Hardware HardwareScript;
    [SerializeField] Score ScoreScript;
    [SerializeField] GameTimer TimerScript;
    [SerializeField] VisualDrink DrinkSwapScript;
    [SerializeField] DrinkAnimations DrinkAnimationsScript;

    //Other Variables
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
                    TimerScript.Rounds++;
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
        Debug.Log("Drink Check !");

        if (TimerScript.GameActive)
        {
            Debug.Log("Drink Check & Game Active");
            
            if (HardwareScript.SelectedVessel == HardwareScript.RequiredVessel && HardwareScript.SelectedIngredient == HardwareScript.RequiredIngredient && HardwareScript.SelectedLiquid == HardwareScript.RequiredLiquid)
            {
                FeedbackAnimator.SetBool("Pos", true);

                if (ScoreScript.FastPreparation())
                {
                    ScoreScript.BonusOrders += 1;
                    TimerScript.ElapsedTime -= 10f;
                    
                }
                else if (!ScoreScript.FastPreparation())
                {
                    ScoreScript.GoodOrders += 1;
                }
            }
            else
            {
                FeedbackAnimator.SetBool("Neg", true);

                ScoreScript.BadOrders += 1;
                TimerScript.ElapsedTime += 5f;
            }

            HardwareScript.PreviousVessel = HardwareScript.RequiredVessel;
            HardwareScript.PreviousIngredient = HardwareScript.RequiredIngredient;
            HardwareScript.PreviousLiquid = HardwareScript.RequiredLiquid;

            HardwareScript.ResetRound();
        }
    }
}
