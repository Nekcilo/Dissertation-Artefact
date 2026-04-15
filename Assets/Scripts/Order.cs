using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    //Order Arrays
    string[] DrinkOrder = {"Black Tea", "Milk Tea", "Black Coffee", "White Coffee", "Hot Chocolate"};

    //Selection Arrays
    string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //Required Values
    [SerializeField] string RequiredIngredient;
    [SerializeField] string RequiredLiquid;

    //DebugText
    [SerializeField] TMP_Text DebugText2; //Ingredient
    [SerializeField] TMP_Text DebugText3; //Liquid
    [SerializeField] TMP_Text DebugText4; //Rotation

    //Object References
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;

    [SerializeField] SpriteRenderer PourLiquid;
    [SerializeField] SpriteRenderer CupLiquid;

    //Animator Referencees
    [SerializeField] public Animator CupAnimator;
    [SerializeField] public Animator PourAnimator;
    [SerializeField] public Animator FeedbackAnimator;

    //Public Variables
    [SerializeField] public int RotValue;
    [SerializeField] public string SelectedLiquid;
    public bool drinkFull;

    //Private Variables
    string SelectedIngredient;
    bool LiquidPoured;
    string PreviousIngredient;
    string PreviousLiquid;

    private void Awake()
    {
        Reset();
    }

    private int RandomInt()
    {
        return Random.Range(0, DrinkOrder.Length); //The maximum parameter is exclusive
    }

    public void Reset()
    {
        Debug.Log("Reset");

        //Reset Variables
        SelectedIngredient = IngredientSelection[0];
        SelectedLiquid = LiquidSelection[0];

        RequiredIngredient = IngredientSelection[0];
        RequiredLiquid = LiquidSelection[0];

        RotValue = 0;
        drinkFull = false;
        LiquidPoured = false;

        //Reset Fill Animation
        CupAnimator.SetBool("AnimIsPouring", false);

        FeedbackAnimator.SetBool("Pos", false);
        FeedbackAnimator.SetBool("Neg", false);

        //Reset Display Text
        DebugText2.text = IngredientSelection[0];
        DebugText3.text = LiquidSelection[0];

        //New Order
        CustomerOrder(DrinkOrder[RandomInt()]);
    }

    void CustomerOrder(string CustOrder)
    {
        int Rnd;

        switch (CustOrder)
        {    
            case "Black Tea":
                //  Mug + Tea bag + Water = Black Tea
                RequiredIngredient = IngredientSelection[1];
                RequiredLiquid = LiquidSelection[1]; //Water

                break;

            case "Milk Tea":
                Rnd = Random.Range(0, 2); //The maximum parameter is exclusive

                RequiredIngredient = IngredientSelection[1];

                if (Rnd == 0)
                {
                    //  Mug + Tea bag + Cow Milk = Milk Tea
                    RequiredLiquid = LiquidSelection[2]; //Cow
                }
                else if (Rnd == 1)
                {
                    //  Mug + Tea bag + Oat Milk = Milk Tea
                    RequiredLiquid = LiquidSelection[3]; //Oat
                }

                break;

            case "Black Coffee":
                //  Mug + Coffee + Water = Black Coffee
                RequiredIngredient = IngredientSelection[2];
                RequiredLiquid = LiquidSelection[1]; //Water

                break;

            case "White Coffee":
                Rnd = Random.Range(0, 2);

                RequiredIngredient = IngredientSelection[2];

                if (Rnd == 0)
                {
                    //  Mug + Coffee + Cow Milk = White Coffee
                    RequiredLiquid = LiquidSelection[2]; //Cow
                }
                else if (Rnd == 1)
                {
                    //  Mug + Coffee + Oat Milk = White Coffee
                    RequiredLiquid = LiquidSelection[3]; //Oat
                }

                break;

            case "Hot Chocolate":
                Rnd = Random.Range(0, 3);

                RequiredIngredient = IngredientSelection[3];

                if (Rnd == 0)
                {
                    //  Mug + Chocolate + Water = Hot Chocolate
                    RequiredLiquid = LiquidSelection[1]; //Water
                }
                else if (Rnd == 1)
                {
                    //  Mug + Chocolate + Cow Milk = Hot Chocolate
                    RequiredLiquid = LiquidSelection[2]; //Cow
                }
                else if (Rnd == 2)
                {
                    //  Mug + Chocolate + Oat Milk = Hot Chocolate
                    RequiredLiquid = LiquidSelection[3]; //Oat
                }

                break;

            default:
                RequiredIngredient = "None";
                RequiredLiquid = "None";
                break;
        }

        if (PreviousIngredient == RequiredIngredient || PreviousLiquid == RequiredLiquid)
        {
            CustomerOrder(DrinkOrder[RandomInt()]);
        }
        else
        {
            DisplayOrder(CustOrder, RequiredLiquid);
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

    public void ButtonCheck(bool ButtonPressed, int ButtonIdentifier)
    {             
        if (!LiquidPoured)
        {
            DebugText3.text = (LiquidSelection[ButtonIdentifier]);
            SelectedLiquid = LiquidSelection[ButtonIdentifier];
        }
    }

    public void NFC(string NFCID)
    {

        if (!LiquidPoured)
        {        
            switch (NFCID)
            {

                case "04 0D 66 4C 9E 61 80":
                    //Tea Bag
                    SelectedIngredient = IngredientSelection[1];
                    PourLiquid.color = new Color32(207, 163, 114, 255);
                    CupLiquid.color = new Color32(207, 163, 114, 255);
                    break;

                case "04 39 46 4C 9E 61 80":
                    //Coffee
                    SelectedIngredient = IngredientSelection[2];
                    PourLiquid.color = new Color32(70, 41, 25, 255);
                    CupLiquid.color = new Color32(70, 41, 25, 255);
                    break;

                case "04 5A 45 4C 9E 61 80":
                    //Chocolate
                    SelectedIngredient = IngredientSelection[3];
                    PourLiquid.color = new Color32(130, 80, 42, 255);
                    CupLiquid.color = new Color32(130, 80, 42, 255);
                    break;

                default:
                    //Nothing Detected
                    SelectedIngredient = IngredientSelection[0];
                    break;
            }

            DebugText2.text = (SelectedIngredient);
        }

    }

    public void Rotation(int RawRotation)
    {
        if (SelectedIngredient != "None" && SelectedLiquid != "None")
        {
            RotValue = (RawRotation * 360) / 1023;
            DebugText4.text = (RotValue.ToString());

            //5 = pour threshold;
            if (RotValue > 5 && !drinkFull)
            {
                LiquidPoured = true;
                PourAnimator.SetBool("AnimIsPouring", true);
            }
            if (drinkFull)
            {
                DrinkCheck();
            }
        }
    }

    void DrinkCheck()
    {

        if (SelectedIngredient == RequiredIngredient && SelectedLiquid == RequiredLiquid)
        { 
            Debug.Log("Correct Liquid");
            Debug.Log("Correct Ingredient");

            FeedbackAnimator.SetBool("Pos", true);
        }
        else
        {
            FeedbackAnimator.SetBool("Neg", true);
        }

        PreviousIngredient = RequiredIngredient;
        PreviousLiquid = RequiredLiquid;
    }

}
