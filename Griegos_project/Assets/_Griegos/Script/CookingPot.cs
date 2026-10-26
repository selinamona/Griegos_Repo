using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public int requiredIngredients = 3;

    // Your radial timing UI
    public GameObject radialTimingUI;

    // Ingredients currently accepted by the pot
    private List<WorldItem> ingredients = new List<WorldItem>();

    public bool AddIngredient(WorldItem item)
    {
        if (item == null)
            return false;

        // Make sure a recipe has been selected
        if (RecipeManager.instance == null)
        {
            Debug.Log("No RecipeManager found!");
            return false;
        }

        // Check if the ingredient belongs to the selected recipe
        if (!RecipeManager.instance.IsIngredientCorrect(item.itemName))
        {
            Debug.Log(item.itemName + " is NOT part of the selected recipe!");

            return false;
        }

        // Don't allow the same ingredient twice
        if (ingredients.Contains(item))
        {
            Debug.Log(item.itemName + " is already in the pot!");

            return false;
        }

        // Accept the ingredient
        ingredients.Add(item);

        Debug.Log(item.itemName + " was accepted!");

        CheckRecipe();

        return true;
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
        Debug.Log("All 3 ingredients are correct! Starting radial QTE!");

        // Show the radial timing UI
        radialTimingUI.SetActive(true);

        // Find the pointer controller
        PointerController pointer =
            radialTimingUI.GetComponentInChildren<PointerController>();

        if (pointer != null)
        {
            // Reset and start the pointer
            pointer.StartQTE();
        }
        else
        {
            Debug.LogWarning("PointerController was not found inside the Radial Timing UI!");
        }
    }
}