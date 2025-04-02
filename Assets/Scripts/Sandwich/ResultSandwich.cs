using Ingredient;
using Sandwich;
using UnityEngine;
public class ResultSandwich : GrabbableObjectBase
{
    [Header("Price")]
    public float sandwichPrice;
    [Space(10)]
    public SandwichItem sandwichItem = new();
    // Public method to set the sandwich item data
    public void SetSandwichItem(SandwichItem newSandwich)
    {
        sandwichItem = newSandwich;
    }
}
