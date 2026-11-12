
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public int requiredIngredients = 3;

    public GameObject radialTimingUI;
    public PointerController pointer;

    [Header("Finished Dishes")]
    public GameObject fishPrefab;
    public GameObject meatballsPrefab;
    public GameObject saladPrefab;

    public Transform dishSpawnPoint;

    private List<WorldItem> ingredients = new List<WorldItem>();
    private bool cookingFinished = false;

    public bool AddIngredient(WorldItem item)
    {
        if (item == null) return false;

        if (RecipeManager.instance == null)
        {
            Debug.LogError("No RecipeManager found!");
            return false;
        }

        if (!RecipeManager.instance.IsIngredientCorrect(item.itemName))
        {
            Debug.Log(item.itemName + " is NOT part of the recipe!");
            return false;
        }

        if (ingredients.Contains(item))
            return false;

        ingredients.Add(item);

        item.gameObject.SetActive(false);

        CheckRecipe();
        return true;
    }

    void CheckRecipe()
    {
        if (ingredients.Count >= requiredIngredients)
            StartRadialQTE();
    }

    void StartRadialQTE()
    {
        if (cookingFinished) return;

        radialTimingUI.SetActive(true);
        pointer.StartQTE();
    }

    public void FinishCooking(string cookingResult)
    {
        if (cookingFinished) return;

        cookingFinished = true;
        radialTimingUI.SetActive(false);

        GameObject selectedPrefab = null;

        string dishName = RecipeManager.instance.selectedDishName;

        if (dishName == "Fish")
        {
            selectedPrefab = fishPrefab;
        }
        else if (dishName == "Meatballs")
        {
            selectedPrefab = meatballsPrefab;
        }
        else if (dishName == "Salad")
        {
            selectedPrefab = saladPrefab;
        }

        if (selectedPrefab == null)
        {
            Debug.LogError("Finished dish prefab is missing!");
            return;
        }

        Transform spawnPoint = dishSpawnPoint != null
            ? dishSpawnPoint
            : transform;

        GameObject dish = Instantiate(
            selectedPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        FinishedDishPickup pickup =
            dish.GetComponent<FinishedDishPickup>();

        if (pickup != null)
        {
            pickup.cookingResult = cookingResult;
        }

        Debug.Log(dishName + " spawned with result: " + cookingResult);
    }
}