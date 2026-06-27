using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualDrink : MonoBehaviour
{
    [Header("Vessel References")]
    [SerializeField] GameObject Mug;
    [SerializeField] GameObject Cup;

    [Header("Insert References")]
    [SerializeField] GameObject MugInserts;
    [SerializeField] GameObject CupInserts;

    [Header("Mask References")]
    [SerializeField] Transform MugMask;
    [SerializeField] Transform CupMask;

    [Header("GameObject Arrays")]
    public GameObject[] Vessel = {};
    public GameObject[] Insert = {};
    public GameObject[] Liquid = {};

    [Header("Script References")]
    [SerializeField] Hardware HardwareScript;
    [SerializeField] Order OrderScript;

    public bool DrinkPouring = false;
    public bool ready = false;

    public Vector3 TargetMaskScale = Vector3.zero;
    public int TargetIndex = -1;
    public Transform ActiveMask = null;

    private Vector3 OriginalMugMaskScale, OriginalCupMaskScale;

    private float FillDuration = 2.0f;

    private void Awake()
    {
        List<GameObject> list = new List<GameObject>();
        
        //Set Vessel Array
        list.Add(Mug);
        list.Add(Cup);
        Vessel = list.ToArray();

        list.Clear();

        //Set Insert Array
        foreach (Transform go in MugInserts.transform)
        {
            list.Add(go.gameObject);
        }
        Insert = list.ToArray();

        foreach (Transform go in CupInserts.transform)
        {
            list.Add(go.gameObject);
        }
        Insert = list.ToArray();

        OriginalMugMaskScale = MugMask.localScale;
        OriginalCupMaskScale = CupMask.localScale;

        //VESSEL ARRAY INDEX
        //Index 0: Mug
        //Index 1: Cup


        //INSERT ARRAY INDEX
        //Index 0: Black Coffee (Mug)
        //Index 1: White Coffee(Mug)
        //Index 2: Black Tea(Mug)
        //Index 3: Milk Tea(Mug)
        //Index 4: Hot Chocolate(Water) (Mug)
        //Index 5: Hot Chocolate(Mug)

        //Index 6: Iced Black Coffee(Cup)
        //Index 7: Iced White Coffee(Cup)
        //Index 8: Iced Black Tea(Cup)
        //Index 9: Iced Milk Tea(Cup)
        //Index 10: Iced Chocolate Milk(Cup)
        //Index 11: Chocolate Milk(Cup)


        ////Debug Checks
        //for (int i = 0; i < Vessel.Length; i++)
        //{
        //    Debug.Log("Index " + i + ": " + Vessel[i]);
        //}
        //for (int i = 0; i < Insert.Length ; i++)
        //{
        //    Debug.Log("Index " + i + ": " + Insert[i]);
        //}
    }

    public void ResetVisual()
    {
        StopAllCoroutines();

        VesselSwap();
        IngredientSwap();
        LiquidSwap();
        DrinkSwap();
    }

    public void VesselSwap() //For Visually Changing the Vessel
    {
        switch (HardwareScript.SelectedVessel)
        {
            case "Mug":
                Vessel[0].SetActive(true);
            break;

            case "Cup":
                Vessel[1].SetActive(true);
                break;

            default:
                Vessel[0].SetActive(false);
                Vessel[1].SetActive(false);
                break;
        }
    }
    
    public void IngredientSwap() //For Visually Changing the Ingredient Indicator
    {
        Debug.Log("IngredientSwap, SelectedIngredient: " + HardwareScript.SelectedIngredient);
    }

    public void LiquidSwap() //For Visually Changing the Liquid Indicator AND Liquid Pour Colour
    {
        if (HardwareScript.SelectedLiquid == OrderScript.LiquidSelection[1]) //Water
        {
            OrderScript.PourLiquid.color = new Color32(137, 224, 236, 50);
        }
        if (HardwareScript.SelectedLiquid == OrderScript.LiquidSelection[2]) //Cow Milk
        {
            OrderScript.PourLiquid.color = new Color32(225, 225, 225, 255);
        }
        if (HardwareScript.SelectedLiquid == OrderScript.LiquidSelection[3]) //Oat Milk
        {
            OrderScript.PourLiquid.color = new Color32(217, 191, 165, 255);
        }
    }

    public void DrinkSwap() //For Visually Changing the Insert of the drink as it is rising
    {
        for (int i = 0; i < Insert.Length; i++)
        {
            Insert[i].SetActive(false);
        }

        if (HardwareScript.SelectedVessel == "None" ||
            HardwareScript.SelectedIngredient == "None" ||
            HardwareScript.SelectedLiquid == "None")
        {
            return;
        }

        TargetIndex = -1;
        ActiveMask = null;
        TargetMaskScale = Vector3.zero;

        if (HardwareScript.SelectedVessel == "Mug")
        {
            ActiveMask = MugMask;
            TargetMaskScale = OriginalMugMaskScale;

            if (HardwareScript.SelectedIngredient == "Coffee")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 0 : 1;
            }
            else if (HardwareScript.SelectedIngredient == "Tea")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 2 : 3;
            }
            else if (HardwareScript.SelectedIngredient == "Chocolate")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 4 : 5;
            }
        }
        else if (HardwareScript.SelectedVessel == "Cup")
        {
            ActiveMask = CupMask;
            TargetMaskScale = OriginalCupMaskScale;

            if (HardwareScript.SelectedIngredient == "Coffee")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 6 : 7; // comedy
            }
            else if (HardwareScript.SelectedIngredient == "Tea")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 8 : 9;
            }
            else if (HardwareScript.SelectedIngredient == "Chocolate")
            {
                TargetIndex = (HardwareScript.SelectedLiquid == "Water") ? 10 : 11;
            }
        }

        if (TargetIndex != -1 && TargetIndex < Insert.Length && ActiveMask != null)
        {
            Insert[TargetIndex].SetActive(true);
            ActiveMask.localScale = new Vector3(TargetMaskScale.x, 0f, TargetMaskScale.y);

            ready = true;
        }
    }

    public IEnumerator FillMask(Transform Mask, Vector3 TargetScale)
    {
        float ElapsedTime = 0f;

        Mask.localScale = new Vector3(TargetScale.x, 0f, TargetScale.y);
        while (ElapsedTime < FillDuration)
        {
            ElapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(0f, TargetScale.y, ElapsedTime / FillDuration);
            Mask.localScale = new Vector3(TargetScale.x, newY, TargetScale.z);

            yield return null;
        }
        Mask.localScale = TargetScale;

        ready = false;
        DrinkPouring = false;
        HardwareScript.drinkFull = true;
        OrderScript.PourAnimator.SetBool("AnimIsPouring", false);
    }
}
