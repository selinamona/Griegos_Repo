using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    public SatisfactionTimer satisfactionTimer;

    // The ingredients required for the currently selected recipe
    public string[] selectedIngredients;

    void Awake()
    {
        instance = this;
    }

    // Choose Recipe 1
    public void ChooseRecipe1()
    {
        selectedIngredients = new string[]
        {
            "Meat",
            "Vegetables",
            "Sauce"
        };

        Debug.Log("Recipe 1 selected!");

        satisfactionTimer.StartTimer();
    }

    // Choose Recipe 2
    public void ChooseRecipe2()
    {
        selectedIngredients = new string[]
        {
            "Vegetables",
            "Oil",
            "Cheese"
        };

        Debug.Log("Recipe 2 selected!");

        satisfactionTimer.StartTimer();
    }

    // Choose Recipe 3
    public void ChooseRecipe3()
    {
        selectedIngredients = new string[]
        {
            "Fish",
            "Vegetables",
            "Potatoes"
        };

        Debug.Log("Recipe 3 selected!");

        satisfactionTimer.StartTimer();
    }

    // Checks if an ingredient belongs to the selected recipe
    public bool IsIngredientCorrect(string ingredientName)
    {
        if (selectedIngredients == null)
            return false;

        foreach (string ingredient in selectedIngredients)
        {
            if (ingredient == ingredientName)
            {
                return true;
            }
        }

        return false;
    }
}