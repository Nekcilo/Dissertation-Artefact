using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VisualIngredientSwitcher : MonoBehaviour
{
    public GameObject ChocolateIngredient, TeaIngredient, CoffeeIngredient;

    public SpriteRenderer currentIngredientSR;

    [SerializeField] Hardware HardwareScript;

    private string CurrentIngredient = "None";

    private void Start()
    {
        UpdateVisuals("None");
    }

    private void Update()
    {
        if (HardwareScript != null && HardwareScript.SelectedIngredient != CurrentIngredient)
        {
            CurrentIngredient = HardwareScript.SelectedIngredient;
            UpdateVisuals(CurrentIngredient);
        }
    }

    public void UpdateVisuals(string IngerdientName)
    {
        if (ChocolateIngredient != null) ChocolateIngredient.SetActive(false);
        if (TeaIngredient != null) TeaIngredient?.SetActive(false);
        if (CoffeeIngredient != null) CoffeeIngredient.SetActive(false);

        switch (IngerdientName)
        {
            case "Chocolate":
                if (ChocolateIngredient != null)
                {
                    ChocolateIngredient.SetActive(true);
                    currentIngredientSR = ChocolateIngredient.GetComponent<SpriteRenderer>();
                }
                break;

            case "Tea":
                if (TeaIngredient != null)
                {
                    TeaIngredient.SetActive(true);
                    currentIngredientSR = TeaIngredient.GetComponent<SpriteRenderer>();
                }
                break;

            case "Coffee":
                if (CoffeeIngredient != null)
                {
                    CoffeeIngredient.SetActive(true);
                    currentIngredientSR = CoffeeIngredient.GetComponent<SpriteRenderer>();
                }
                break;

            case "None":
            default:
                break;
        }
    }
}
