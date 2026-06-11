using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDrink : MonoBehaviour
{
    [Header("Vessel References")]
    [SerializeField] GameObject Mug;
    [SerializeField] GameObject Cup;

    [Header("Insert References")]
    [SerializeField] GameObject MugInserts;
    [SerializeField] GameObject CupInserts;

    [Header("GameObject Arrays")]
    public GameObject[] Vessel = {};
    public GameObject[] Insert = {};
    public GameObject[] Liquid = {};

    [Header("Script References")]
    [SerializeField] Hardware HardwareScript;
    [SerializeField] Order OrderScript;

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
            OrderScript.PourLiquid.color = new Color32(207, 163, 114, 255);
            OrderScript.CupLiquid.color = new Color32(207, 163, 114, 255);
        }
        if (HardwareScript.SelectedLiquid == OrderScript.LiquidSelection[2]) //Cow Milk
        {
            OrderScript.PourLiquid.color = new Color32(70, 41, 25, 255);
            OrderScript.CupLiquid.color = new Color32(70, 41, 25, 255);
        }
        if (HardwareScript.SelectedLiquid == OrderScript.LiquidSelection[3]) //Oat Milk
        {
            OrderScript.PourLiquid.color = new Color32(130, 80, 42, 255);
            OrderScript.CupLiquid.color = new Color32(130, 80, 42, 255);
        }
    }

    public void DrinkSwap() //For Visually Changing the Insert of the drink as it is rising
    {
        //HardwareScript.SelectedVessel;
        //HardwareScript.SelectedIngredient;
        //HardwareScript.SelectedLiquid;

        if (HardwareScript.SelectedVessel == "Cup")
        {

        }
        else if (HardwareScript.SelectedVessel == "Mug")
        {

        }
        else
        {
            for (int i = 0; i < Insert.Length; i++)
            {
                Insert[i].SetActive(false);
            }
        }
    }

}
