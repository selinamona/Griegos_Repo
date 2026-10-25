using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public int requiredIngredients = 3;

    // Your radial timing bar UI
    public GameObject radialTimingUI;

    private List<WorldItem> ingredients = new List<WorldItem>();

    public void AddIngredient(WorldItem item)
    {
        if (item == null)
            return;

        // Don't add the same ingredient twice
        if (ingredients.Contains(item))
            return;

        ingredients.Add(item);

        Debug.Log("Added " + item.itemName + " to the pot.");

        CheckRecipe();
    }

    void CheckRecipe()
    {
        if (ingredients.Count >= requiredIngredients)
        {
            StartRadialQTE();
        }
    }

    void StartRadialQTE()
    {
        Debug.Log("All 3 ingredients added! Starting radial QTE!");

        radialTimingUI.SetActive(true);
    }
}