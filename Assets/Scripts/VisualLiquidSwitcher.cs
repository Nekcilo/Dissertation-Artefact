using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualLiquidSwitcher : MonoBehaviour
{
    public GameObject WaterLiquid, CowsMilkLiquid, OatMilkLiquid;

    [SerializeField] Hardware HardwareScript;

    private string CurrentLiquid = "None";

    private void Start()
    {
        UpdateVisuals("None");
    }

    private void Update()
    {
        if (HardwareScript != null && HardwareScript.SelectedLiquid != CurrentLiquid)
        {
            CurrentLiquid = HardwareScript.SelectedLiquid;
            UpdateVisuals(CurrentLiquid);
        }
    }

    public void UpdateVisuals(string LiquidType)
    {
        if (WaterLiquid != null) WaterLiquid.SetActive(false);
        if (CowsMilkLiquid != null) CowsMilkLiquid.SetActive(false);
        if (OatMilkLiquid != null) OatMilkLiquid.SetActive(false);

        switch (LiquidType)
        {
            case "Water":
                if (WaterLiquid != null) WaterLiquid.SetActive(true);
                break;

            case "Cow":
                if (CowsMilkLiquid != null) CowsMilkLiquid.SetActive(true);
                break;

            case "Oat":
                if (OatMilkLiquid != null) OatMilkLiquid.SetActive(true);
                break;

            case "None":
            default:
                break;
        }
    }
}
