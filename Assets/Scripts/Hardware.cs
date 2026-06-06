using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardware : MonoBehaviour
{
    //Script References
    [SerializeField] Order OrderScript;

    //Public Variables
    [SerializeField] public int RotValue;
    public bool drinkFull;
    public bool VesselPresent = false;
    public bool IngredientPresent = false;
    public string RequiredVessel, RequiredIngredient, RequiredLiquid;
    public string SelectedVessel, SelectedIngredient, SelectedLiquid;
    public string PreviousVessel, PreviousIngredient, PreviousLiquid;
    public float PreviousVesselTime;
    public float PreviousIngredientTime;

    //Private Variables
    bool LiquidPoured;
    float TimeoutTime = 0.5f;
    int VesselReader;

    private void Awake()
    {
        Reset();
    }

    private void Update()
    {
        if (VesselPresent && (Time.time - PreviousVesselTime > TimeoutTime))
        {
            VesselPresent = false;
            VesselReader = 0;
            Debug.Log("Vessel Removed");
        }
        if (IngredientPresent && (Time.time - PreviousIngredientTime > TimeoutTime))
        {
            IngredientPresent = false;
            Debug.Log("Ingredient Removed");
            OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
        }
    }

    public void Reset()
    {
        Debug.Log("Reset");

        //Reset Variables
        SelectedIngredient = OrderScript.IngredientSelection[0];
        SelectedLiquid = OrderScript.LiquidSelection[0];

        RequiredIngredient = OrderScript.IngredientSelection[0];
        RequiredLiquid = OrderScript.LiquidSelection[0];

        RotValue = 0;
        drinkFull = false;
        LiquidPoured = false;

        VesselPresent = false;
        IngredientPresent = false;

        //Reset Fill Animation
        OrderScript.CupAnimator.SetBool("AnimIsPouring", false);

        OrderScript.FeedbackAnimator.SetBool("Pos", false);
        OrderScript.FeedbackAnimator.SetBool("Neg", false);

        //Reset Display Text
        OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
        OrderScript.DebugText3.text = OrderScript.LiquidSelection[0];

        //New Order
        OrderScript.CustomerOrder(OrderScript.RandomInt());
    }
    public void ButtonCheck(bool ButtonPressed, int ButtonIdentifier)
    {
        if (!LiquidPoured)
        {
            OrderScript.DebugText3.text = (OrderScript.LiquidSelection[ButtonIdentifier]);
            SelectedLiquid = OrderScript.LiquidSelection[ButtonIdentifier];
        }
    }

    public void NFC(string NFCID, int Reader)
    {
        if (NFCID == "04 34 AE 4C 9E 61 80" || NFCID == "04 5A 7D 40 9E 61 80")
        {
            PreviousVesselTime = Time.time;
        }


        if (!LiquidPoured && !VesselPresent)
        {
            switch (NFCID)

            {
                case "04 34 AE 4C 9E 61 80":
                    //Cup
                    Debug.Log("Cup Present");
                    VesselPresent = true;
                    VesselReader = Reader;
                    SelectedVessel = "Cup";
                    PreviousVesselTime = Time.time;
                    break;

                case "04 5A 7D 40 9E 61 80":
                    //Mug
                    Debug.Log("Mug Present");
                    VesselPresent = true;
                    VesselReader = Reader;
                    SelectedVessel = "Mug";
                    PreviousVesselTime = Time.time;
                    break;

                default:
                    break;
            }
        }

        if (!LiquidPoured && VesselPresent && Reader != VesselReader)
        {
            switch (NFCID)
            {

                case "04 0D 66 4C 9E 61 80":
                    //Tea Bag
                    Debug.Log("Ingredient Present");
                    IngredientPresent = true;
                    SelectedIngredient = OrderScript.IngredientSelection[1];
                    OrderScript.PourLiquid.color = new Color32(207, 163, 114, 255);
                    OrderScript.CupLiquid.color = new Color32(207, 163, 114, 255);
                    PreviousIngredientTime = Time.time;
                    break;

                case "04 39 46 4C 9E 61 80":
                    //Coffee
                    Debug.Log("Ingredient Present");
                    IngredientPresent = true;
                    SelectedIngredient = OrderScript.IngredientSelection[2];
                    OrderScript.PourLiquid.color = new Color32(70, 41, 25, 255);
                    OrderScript.CupLiquid.color = new Color32(70, 41, 25, 255);
                    PreviousIngredientTime = Time.time;
                    break;

                case "04 5A 45 4C 9E 61 80":
                    //Chocolate
                    Debug.Log("Ingredient Present");
                    IngredientPresent = true;
                    SelectedIngredient = OrderScript.IngredientSelection[3];
                    OrderScript.PourLiquid.color = new Color32(130, 80, 42, 255);
                    OrderScript.CupLiquid.color = new Color32(130, 80, 42, 255);
                    PreviousIngredientTime = Time.time;
                    break;

                default:
                    //Nothing Detected
                    SelectedIngredient = OrderScript.IngredientSelection[0];
                    break;
            }

            OrderScript.DebugText2.text = (SelectedIngredient);
        }

    }

    public void Rotation(int RawRotation)
    {
        if (SelectedIngredient != "None" && SelectedLiquid != "None")
        {
            RotValue = (RawRotation * 360) / 1023;

            //5 = pour threshold;
            if (RotValue < 205 && !drinkFull)
            {
                LiquidPoured = true;
                OrderScript.PourAnimator.SetBool("AnimIsPouring", true);
            }
            if (drinkFull)
            {
                OrderScript.DrinkCheck();
            }
        }
    }
}
