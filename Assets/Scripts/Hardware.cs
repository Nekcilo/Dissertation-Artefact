using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardware : MonoBehaviour
{
    //Public Variables
    [HideInInspector] public int RotValue;
    public bool drinkFull;

    //NFC Presence Checks
    [HideInInspector] public bool VesselPresent = false;
    [HideInInspector] public bool IngredientPresent = false;

    //Required
    [Header("Required")]
    public string RequiredVessel; public string RequiredIngredient; public string RequiredLiquid;

    //Selected
    [Header("Selected")]
    public string SelectedVessel; public string SelectedIngredient; public string SelectedLiquid;

    //Previous
    [HideInInspector] public string PreviousVessel, PreviousIngredient, PreviousLiquid;

    //Private Variables
    bool LiquidPoured;
    float PreviousVesselTime;
    float PreviousIngredientTime;
    float VesselTimeoutTime = 0.5f;
  //  float IngredientTimeoutTime = 1.2f;
    int VesselReader;

    //Script References
    [Header("Script References")]
    [SerializeField] Order OrderScript;
    [SerializeField] Score ScoreScript;
    [SerializeField] GameTimer TimerScript;
    [SerializeField] VisualDrink DrinkSwapScript;

    private void Update()
    {
        //NFC Removed Timer
        if (VesselPresent && (Time.time - PreviousVesselTime > VesselTimeoutTime))
        {
            VesselPresent = false;
            VesselReader = 0;
            SelectedVessel = OrderScript.VesselSelection[0];
            OrderScript.DebugText1.text = OrderScript.VesselSelection[0];
            DrinkSwapScript.VesselSwap();

            //for ingredient, will timeout when vessel removed to help with consistency issues
            IngredientPresent = false;
            SelectedIngredient = OrderScript.IngredientSelection[0];
            OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
            DrinkSwapScript.IngredientSwap();
        }
       /* if (IngredientPresent && (Time.time - PreviousIngredientTime > IngredientTimeoutTime))
        {
            IngredientPresent = false;
            SelectedIngredient = OrderScript.IngredientSelection[0];
            OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
            DrinkSwapScript.IngredientSwap();
        }*/

        // TEMP REMOVE LATER ISTG DO NOT FORGET
        if (Input.GetKeyDown(KeyCode.Alpha1)) ButtonCheck(true, 1); 
        if (Input.GetKeyDown(KeyCode.Alpha2)) ButtonCheck(true, 2); 
        if (Input.GetKeyDown(KeyCode.Alpha3)) ButtonCheck(true, 3); 

        if (Input.GetKey(KeyCode.Q)) NFC("04 5A 7D 40 9E 61 80", 1); 
        if (Input.GetKey(KeyCode.W)) NFC("04 34 AE 4C 9E 61 80", 1);

        if (Input.GetKey(KeyCode.A)) NFC("04 0D 66 4C 9E 61 80", 2); 
        if (Input.GetKey(KeyCode.S)) NFC("04 39 46 4C 9E 61 80", 2); 
        if (Input.GetKey(KeyCode.D)) NFC("04 5A 45 4C 9E 61 80", 2);

        if (Input.GetKeyDown(KeyCode.Space)) Rotation(100);
    }

    public void ResetRound()
    {
        Debug.Log("Reset Round");

        Debug.Log("Round: " + TimerScript.Rounds);

        //Reset Variables
        SelectedVessel = OrderScript.VesselSelection[0];
        SelectedIngredient = OrderScript.IngredientSelection[0];
        SelectedLiquid = OrderScript.LiquidSelection[0];

        DrinkSwapScript.ResetVisual();

        RequiredVessel = OrderScript.VesselSelection[0];
        RequiredIngredient = OrderScript.IngredientSelection[0];
        RequiredLiquid = OrderScript.LiquidSelection[0];

        RotValue = 0;
        drinkFull = false;
        LiquidPoured = false;

        VesselPresent = false;
        IngredientPresent = false;

        ScoreScript.PreviousTime = 0f;
        OrderScript.RoundStarted = false;

        //Reset Display Text
        OrderScript.DebugText1.text = OrderScript.VesselSelection[0];
        OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
        OrderScript.DebugText3.text = OrderScript.LiquidSelection[0];

        //New Order
        OrderScript.CustomerOrder(OrderScript.RandomInt());
    }

    public void ResetAnim()
    {
        OrderScript.PourAnimator.SetBool("AnimIsPouring", false);

        OrderScript.FeedbackAnimator.SetBool("Pos", false);
        OrderScript.FeedbackAnimator.SetBool("Neg", false);
    }

    public void ButtonCheck(bool ButtonPressed, int ButtonIdentifier)
    {
        if (TimerScript.GameActive)
        {
            if (!LiquidPoured)
            {
                OrderScript.DebugText3.text = (OrderScript.LiquidSelection[ButtonIdentifier]);
                SelectedLiquid = OrderScript.LiquidSelection[ButtonIdentifier];
                DrinkSwapScript.LiquidSwap();
            }
        }
        else if (!TimerScript.GameActive && TimerScript.CooldownTimer())
        {
            if (TimerScript.HasCalculated && ButtonIdentifier == 1)
            {
                TimerScript.Replay = true;
            }
        }
    }

    public void NFC(string NFCID, int Reader)
    {
        if (TimerScript.GameActive)
        {
            if (NFCID == "04 34 AE 4C 9E 61 80" || NFCID == "04 5A 7D 40 9E 61 80")
            {
                PreviousVesselTime = Time.time;
            }

            if (!LiquidPoured && !VesselPresent)
            {
                switch (NFCID)

                {
                    case "04 5A 7D 40 9E 61 80":
                        //Mug
                        VesselPresent = true;
                        VesselReader = Reader;
                        SelectedVessel = OrderScript.VesselSelection[1];
                        PreviousVesselTime = Time.time;
                        break;

                    case "04 34 AE 4C 9E 61 80":
                        //Cup
                        VesselPresent = true;
                        VesselReader = Reader;
                        SelectedVessel = OrderScript.VesselSelection[2];
                        PreviousVesselTime = Time.time;
                        break;

                    default:
                        //Nothing Detected
                        SelectedVessel = OrderScript.VesselSelection[0];
                        break;
                }

                OrderScript.DebugText1.text = (SelectedVessel);
                DrinkSwapScript.VesselSwap();
            }

            if (VesselPresent && Reader != VesselReader)
            {
                switch (NFCID)
                {

                    case "04 0D 66 4C 9E 61 80":
                        //Tea Bag
                        IngredientPresent = true;
                        SelectedIngredient = OrderScript.IngredientSelection[1];
                        PreviousIngredientTime = Time.time;
                        break;

                    case "04 39 46 4C 9E 61 80":
                        //Coffee
                        IngredientPresent = true;
                        SelectedIngredient = OrderScript.IngredientSelection[2];
                        PreviousIngredientTime = Time.time;
                        break;

                    case "04 5A 45 4C 9E 61 80":
                        //Chocolate
                        IngredientPresent = true;
                        SelectedIngredient = OrderScript.IngredientSelection[3];
                        PreviousIngredientTime = Time.time;
                        break;

                    default:
                        //Nothing Detected
                        SelectedIngredient = OrderScript.IngredientSelection[0];
                        break;
                }

                OrderScript.DebugText2.text = (SelectedIngredient);
                DrinkSwapScript.IngredientSwap();
            }
        }    
    }

    public void Rotation(int RawRotation)
    {
        if (TimerScript.GameActive)
        {
            if (SelectedVessel != "None" && SelectedIngredient != "None" && SelectedLiquid != "None")
            {
                RotValue = (RawRotation * 360) / 1023;

                //205 = pour threshold;
                if (RotValue < 205 && !drinkFull)
                {
                    LiquidPoured = true;
                    DrinkSwapScript.DrinkSwap();
                    OrderScript.PourAnimator.SetBool("AnimIsPouring", true);
                }
                else if (drinkFull)
                {
                    OrderScript.DrinkCheck();
                }
            }
        }
    }
}
