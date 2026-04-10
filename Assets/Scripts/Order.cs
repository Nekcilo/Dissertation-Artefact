using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    //Order Arrays
    string[] DrinkOrder = {"Black Tea", "Milk Tea", "Black Coffee", "White Coffee", "Hot Chocolate", "Chocolate Milk"};

    //Selection Arrays
    string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //Required Values
    string RequiredIngredient;
    string RequiredLiquid;

    //DebugText
    [SerializeField] TMP_Text DebugText2; //Ingredient
    [SerializeField] TMP_Text DebugText3; //Liquid
    [SerializeField] TMP_Text DebugText4; //Rotation

    //Object References
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;
    //[SerializeField] RawImage Image;

    [SerializeField] Pouring PouringScript;

    //Public Variables
    [SerializeField] public int RotValue;
    [SerializeField] public string SelectedLiquid;

    //Private Variables
    string CustOrder;
    string SelectedIngredient;
    bool LiquidPoured = false;

    private void Start()
    {
        SelectedIngredient = IngredientSelection[0];
        SelectedLiquid = LiquidSelection[0];

        CustOrder = DrinkOrder[Random.Range(0, DrinkOrder.Length)];
        CustomerOrder();
    }

    void CustomerOrder()
    {
        int Rnd;

        switch (CustOrder)
        {    
            case "Black Tea":
                //  Mug + Tea bag + Water = Black Tea
                RequiredIngredient = IngredientSelection[1];
                RequiredLiquid = LiquidSelection[1];
                
                DisplayOrder(RequiredLiquid);
                break;

            case "Milk Tea":
                Rnd = Random.Range(0, 1);

                RequiredIngredient = IngredientSelection[1];

                if (Rnd == 0)
                {
                    //  Mug + Tea bag + Cow Milk = Milk Tea
                    RequiredLiquid = LiquidSelection[2];
                }
                else if (Rnd == 1)
                {
                    //  Mug + Tea bag + Oat Milk = Milk Tea
                    RequiredLiquid = LiquidSelection[3];
                }

                DisplayOrder(RequiredLiquid);
                break;

            case "Black Coffee":
                //  Mug + Coffee + Water = Black Coffee
                RequiredIngredient = IngredientSelection[2];
                RequiredLiquid = LiquidSelection[1];

                DisplayOrder(RequiredLiquid);
                break;

            case "White Coffee":
                Rnd = Random.Range(0, 1);

                RequiredIngredient = IngredientSelection[2];

                if (Rnd == 0)
                {
                    //  Mug + Coffee + Cow Milk = White Coffee
                    RequiredLiquid = LiquidSelection[2];
                }
                else if (Rnd == 1)
                {
                    //  Mug + Coffee + Oat Milk = White Coffee
                    RequiredLiquid = LiquidSelection[3];
                }

                DisplayOrder(RequiredLiquid);
                break;

            case "Hot Chocolate":
                Rnd = Random.Range(0, 2);

                RequiredIngredient = IngredientSelection[3];

                if (Rnd == 0)
                {
                    //  Mug + Chocolate + Water = Hot Chocolate
                    RequiredLiquid = LiquidSelection[1];
                }
                else if (Rnd == 1)
                {
                    //  Mug + Chocolate + Cow Milk = Hot Chocolate
                    RequiredLiquid = LiquidSelection[2];
                }
                else if (Rnd == 2)
                {
                    //  Mug + Chocolate + Oat Milk = Hot Chocolate
                    RequiredLiquid = LiquidSelection[3];
                }

                DisplayOrder(RequiredLiquid);
                break;

            default:
                RequiredIngredient = "None";
                RequiredLiquid = "None";
                break;
        }
    }

    void DisplayOrder(string Liquid)
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

        //if (LiquidSelection[ButtonIdentifier] == RequiredLiquid)
        //{
        //    Debug.Log("Correct Liquid");
        //}
    }

    public void NFC(string NFCID)
    {
        //Debug.Log("NFC ID: " + NFCID);

        if (!LiquidPoured)
        {        
            switch (NFCID)
            {

                case "04 0D 66 4C 9E 61 80":
                    //Tea Bag
                    SelectedIngredient = IngredientSelection[1];
                    break;

                case "04 39 46 4C 9E 61 80":
                    //Coffee
                    SelectedIngredient = IngredientSelection[2];
                    break;

                case "04 5A 45 4C 9E 61 80":
                    //Chocolate
                    SelectedIngredient = IngredientSelection[3];
                    break;

                default:
                    //Nothing Detected
                    SelectedIngredient = IngredientSelection[0];
                    break;
            }

            DebugText2.text = (SelectedIngredient);
        }

        //if (SelectedIngredient == RequiredIngredient)
        //{
        //    Debug.Log("Correct Ingredient");
        //}

    }

    public void Rotation(int RawRotation)
    {
        if (SelectedIngredient != "None" && SelectedLiquid != "None")
        {
            RotValue = (RawRotation * 360) / 1023;
            //Image.transform.rotation = Quaternion.Euler(0f, 0f, RotValue);
            DebugText4.text = (RotValue.ToString());

            //5 = pour threshold;
            if (RotValue > 5)
            {
                LiquidPoured = true;
            }
            else if (RotValue < 5)
            {
                LiquidPoured = false;
            }

            PouringScript.PourCheck(LiquidPoured);

            //if cupfull
            //{
            DrinkCheck();
            //}
        }
    }

    void DrinkCheck()
    {
        if (SelectedIngredient == RequiredIngredient && SelectedLiquid == RequiredLiquid)
        { 
            //Debug.Log("Correct Liquid");
            //Debug.Log("Correct Ingredient");
        }
    }

}
