using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drinks", menuName = "Drinks")]

public class DrinkDefiniton : ScriptableObject
{
    public string DrinkName, RequiredVessel, RequiredIngredient, RequiredLiquid;
}
