
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public int requiredIngredients = 3;

    public GameObject radialTimingUI;

    // NEW: Finished dish settings
    public GameObject finishedDishPrefab;
    public Transform dishSpawnPoint;

    private List<WorldItem> ingredients = new List<WorldItem>();

    private bool cookingFinished = false;

    public bool AddIngredient(WorldItem item)
    {
        if (item == null)
            return false;

        if (RecipeManager.instance == null)
        {
            Debug.Log("No RecipeManager found!");
            return false;
        }

        if (!RecipeManager.instance.IsIngredientCorrect(item.itemName))
        {
            Debug.Log(item.itemName + " is NOT part of the selected recipe!");
            return false;
        }

        if (ingredients.Contains(item))
        {
            Debug.Log(item.itemName + " is already in the pot!");
            return false;
        }

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

        radialTimingUI.SetActive(true);

        PointerController pointer =
            radialTimingUI.GetComponentInChildren<PointerController>();

        if (pointer != null)
        {
            pointer.StartQTE();
        }
        else
        {
            Debug.LogWarning("PointerController was not found inside the Radial Timing UI!");
        }
    }

    // NEW: Called when the radial QTE succeeds
    public void FinishCooking()
    {
        if (cookingFinished)
            return;

        cookingFinished = true;

        // Hide the radial timing UI
        radialTimingUI.SetActive(false);

        // Choose where the dish appears
        Transform spawnPoint = dishSpawnPoint != null
            ? dishSpawnPoint
            : transform;

        // Spawn the finished 3D dish
        if (finishedDishPrefab != null)
        {
            Instantiate(
                finishedDishPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            Debug.Log("Finished dish spawned!");
        }
        else
        {
            Debug.LogWarning("Finished Dish Prefab has not been assigned!");
        }
    }
}
