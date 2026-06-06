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
    string[] IngredientSelection = { "None", "Tea", "Coffee", "Chocolate" };
    string[] LiquidSelection = { "None", "Water", "Cow", "Oat" };

    //Required Values
    [SerializeField] string RequiredVessel;
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
    public bool drinkFull;
    public bool VesselPresent = false;
    public bool IngredientPresent = false;

    //Private Variables
    string SelectedVessel, SelectedIngredient, SelectedLiquid;
    bool LiquidPoured;
    
    string PreviousVessel, PreviousIngredient, PreviousLiquid;

    float TimeoutTime = 0.5f;
    public float PreviousVesselTime;
    public float PreviousIngredientTime;
    
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
            DebugText2.text = IngredientSelection[0];
        }
    }

    private int RandomInt()
    {
        return Random.Range(0, DrinkOrder.Count); //The maximum parameter is exclusive
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

        VesselPresent = false;
        IngredientPresent = false;

        //Reset Fill Animation
        CupAnimator.SetBool("AnimIsPouring", false);

        FeedbackAnimator.SetBool("Pos", false);
        FeedbackAnimator.SetBool("Neg", false);

        //Reset Display Text
        DebugText2.text = IngredientSelection[0];
        DebugText3.text = LiquidSelection[0];

        //New Order
        CustomerOrder(RandomInt());
    }

    void CustomerOrder(int CustOrder)
    {
        Definition = DrinkOrder[CustOrder];

        RequiredVessel = Definition.RequiredVessel;
        RequiredIngredient = Definition.RequiredIngredient;
        RequiredLiquid = Definition.RequiredLiquid;

        if (PreviousVessel == RequiredVessel || PreviousIngredient == RequiredIngredient || PreviousLiquid == RequiredLiquid)
        {
            CustomerOrder(RandomInt());
        }
        else
        {
            DisplayOrder(Definition.DrinkName, RequiredLiquid);
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
                    Debug.Log("Vessel Present");
                    VesselPresent = true;
                    VesselReader = Reader;
                    SelectedVessel = "Cup";
                    PreviousVesselTime = Time.time;
                    break;

                case "04 5A 7D 40 9E 61 80":
                    //Mug
                    Debug.Log("Vessel Present");
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
                    SelectedIngredient = IngredientSelection[1];
                    PourLiquid.color = new Color32(207, 163, 114, 255);
                    CupLiquid.color = new Color32(207, 163, 114, 255);
                    PreviousIngredientTime = Time.time;
                    break;

                case "04 39 46 4C 9E 61 80":
                    //Coffee
                    Debug.Log("Ingredient Present");
                    IngredientPresent = true;
                    SelectedIngredient = IngredientSelection[2];
                    PourLiquid.color = new Color32(70, 41, 25, 255);
                    CupLiquid.color = new Color32(70, 41, 25, 255);
                    PreviousIngredientTime = Time.time;
                    break;

                case "04 5A 45 4C 9E 61 80":
                    //Chocolate
                    Debug.Log("Ingredient Present");
                    IngredientPresent = true;
                    SelectedIngredient = IngredientSelection[3];
                    PourLiquid.color = new Color32(130, 80, 42, 255);
                    CupLiquid.color = new Color32(130, 80, 42, 255);
                    PreviousIngredientTime = Time.time;
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
            if (RotValue < 205 && !drinkFull)
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

        if (SelectedVessel == RequiredVessel && SelectedIngredient == RequiredIngredient && SelectedLiquid == RequiredLiquid)
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

        PreviousVessel = RequiredVessel;
        PreviousIngredient = RequiredIngredient;
        PreviousLiquid = RequiredLiquid;
    }

}
