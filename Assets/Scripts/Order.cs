using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{

    public List<DrinkDefiniton> DrinkOrder = new List<DrinkDefiniton>();
    private DrinkDefiniton Definition;

    //Selection Arrays
    public string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    public string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //DebugText
    public TMP_Text DebugText2, DebugText3; //Ingredient //Liquid

    //Object References
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;

    [SerializeField] public SpriteRenderer PourLiquid;
    [SerializeField] public SpriteRenderer CupLiquid;

    //Animator References
    public Animator CupAnimator;
    public Animator PourAnimator;
    public Animator FeedbackAnimator;

    //Script References
    [SerializeField] Hardware HardwareScript;

    public int RandomInt()
    {
        return Random.Range(0, DrinkOrder.Count); //The maximum parameter is exclusive
    }

    public void CustomerOrder(int CustOrder)
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

        if (HardwareScript.SelectedVessel == HardwareScript.RequiredVessel && HardwareScript.SelectedIngredient == HardwareScript.RequiredIngredient && HardwareScript.SelectedLiquid == HardwareScript.RequiredLiquid)
        {
            Debug.Log("Correct Vessel");
            Debug.Log("Correct Liquid");
            Debug.Log("Correct Ingredient");

            FeedbackAnimator.SetBool("Pos", true);
        }
        else
        {
            FeedbackAnimator.SetBool("Neg", true);
        }

        HardwareScript.PreviousVessel = HardwareScript.RequiredVessel;
        HardwareScript.PreviousIngredient = HardwareScript.RequiredIngredient;
        HardwareScript.PreviousLiquid = HardwareScript.RequiredLiquid;
    }

}
