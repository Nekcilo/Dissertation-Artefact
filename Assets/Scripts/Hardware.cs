using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardware : MonoBehaviour
{
    //Public Variables
    [HideInInspector] public int RotValue;
    [HideInInspector] public bool drinkFull;

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
    float TimeoutTime = 0.5f;
    int VesselReader;

    //Script References
    [Header("Script References")]
    [SerializeField] Order OrderScript;
    [SerializeField] Score ScoreScript;
    [SerializeField] GameTimer TimerScript;

    private void Update()
    {
        //NFC Removed Timer
        if (VesselPresent && (Time.time - PreviousVesselTime > TimeoutTime))
        {
            VesselPresent = false;
            VesselReader = 0;
        }
        if (IngredientPresent && (Time.time - PreviousIngredientTime > TimeoutTime))
        {
            IngredientPresent = false;
            OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
        }
    }

    public void ResetRound()
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

        ScoreScript.PreviousTime = 0f;
        OrderScript.RoundStarted = false;

        TimerScript.HasCalculated = false;
        TimerScript.CooldownTime = 0f;

        //Reset Display Text
        OrderScript.DebugText1.text = "None";
        OrderScript.DebugText2.text = OrderScript.IngredientSelection[0];
        OrderScript.DebugText3.text = OrderScript.LiquidSelection[0];

        //New Order
        OrderScript.CustomerOrder(OrderScript.RandomInt());
    }

    public void ResetAnim()
    {
        Debug.Log("Reset Anim");

        OrderScript.CupAnimator.SetBool("AnimIsPouring", false);

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
            }
        }
        else if (!TimerScript.GameActive)
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
                    case "04 34 AE 4C 9E 61 80":
                        //Cup
                        VesselPresent = true;
                        VesselReader = Reader;
                        SelectedVessel = "Cup";
                        PreviousVesselTime = Time.time;
                        break;

                    case "04 5A 7D 40 9E 61 80":
                        //Mug
                        VesselPresent = true;
                        VesselReader = Reader;
                        SelectedVessel = "Mug";
                        PreviousVesselTime = Time.time;
                        break;

                    default:
                        //Nothing Detected
                        SelectedVessel = "None";
                        break;
                }

                OrderScript.DebugText1.text = (SelectedVessel);
            }

            if (!LiquidPoured && VesselPresent && Reader != VesselReader)
            {
                switch (NFCID)
                {

                    case "04 0D 66 4C 9E 61 80":
                        //Tea Bag
                        IngredientPresent = true;
                        SelectedIngredient = OrderScript.IngredientSelection[1];
                        OrderScript.PourLiquid.color = new Color32(207, 163, 114, 255);
                        OrderScript.CupLiquid.color = new Color32(207, 163, 114, 255);
                        PreviousIngredientTime = Time.time;
                        break;

                    case "04 39 46 4C 9E 61 80":
                        //Coffee
                        IngredientPresent = true;
                        SelectedIngredient = OrderScript.IngredientSelection[2];
                        OrderScript.PourLiquid.color = new Color32(70, 41, 25, 255);
                        OrderScript.CupLiquid.color = new Color32(70, 41, 25, 255);
                        PreviousIngredientTime = Time.time;
                        break;

                    case "04 5A 45 4C 9E 61 80":
                        //Chocolate
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
