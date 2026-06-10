using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        //Debug Checks
        for (int i = 0; i < Vessel.Length; i++)
        {
            Debug.Log("Index " + i + ": " + Vessel[i]);
        }
        for (int i = 0; i < Insert.Length ; i++)
        {
            Debug.Log("Index " + i + ": " + Insert[i]);
        }
    }

    public void VesselSwap(string SelectedVessel) //For Visually Changing the Vessel
    {
        switch (SelectedVessel)
        {
            case "Tea": //Incorrect
                Vessel[0].SetActive(true);
            break;

            case "Coffee": //Incorrect
                Vessel[1].SetActive(true);
                break;

            default:
                Vessel[0].SetActive(false);
                Vessel[1].SetActive(false);
                break;
        }
    }
    
    public void IngredientSwap(string SelectedIngredient) //For Visually Changing the Ingredient Indicator
    {
        Debug.Log("IngredientSwap, SelectedIngredient: " + SelectedIngredient);
    }

    public void LiquidSwap(string SelectedLiquid) //For Visually Changing the Liquid Indicator AND Liquid Pour Colour
    {
        if (SelectedLiquid == OrderScript.LiquidSelection[1]) //Water
        {
            OrderScript.PourLiquid.color = new Color32(207, 163, 114, 255);
            OrderScript.CupLiquid.color = new Color32(207, 163, 114, 255);
        }
        if (SelectedLiquid == OrderScript.LiquidSelection[2]) //Cow Milk
        {
            OrderScript.PourLiquid.color = new Color32(70, 41, 25, 255);
            OrderScript.CupLiquid.color = new Color32(70, 41, 25, 255);
        }
        if (SelectedLiquid == OrderScript.LiquidSelection[3]) //Oat Milk
        {
            OrderScript.PourLiquid.color = new Color32(130, 80, 42, 255);
            OrderScript.CupLiquid.color = new Color32(130, 80, 42, 255);
        }
    }

    public void DrinkSwap(string SelectedVessel, string SelectedIngredient, string SelectedLiquid) //For Visually Changing the Insert of the drink as it is rising
    {
        if (SelectedVessel == "Cup")
        {

        }
        else if (SelectedVessel == "Mug")
        {

        }
    }

}
