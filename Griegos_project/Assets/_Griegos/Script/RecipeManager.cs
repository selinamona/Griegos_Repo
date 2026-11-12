
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    public SatisfactionTimer satisfactionTimer;

    // Ingredients for the selected recipe
    public string[] selectedIngredients;

    // Name of the selected finished dish
    public string selectedDishName;

    // Prevents the satisfaction timer from restarting
    private bool timerStarted = false;

    void Awake()
    {
        instance = this;
        selectedIngredients = null;
        selectedDishName = "";
        timerStarted = false;
    }

    // Recipe 1 - Fish
    public void ChooseRecipe1()
    {
        SelectRecipe(new string[]
        {
            "Fish",
            "Vegetables",
            "Potatos"
        }, "Fish");
    }

    // Recipe 2 - Meatballs
    public void ChooseRecipe2()
    {
        SelectRecipe(new string[]
        {
            "Vegetables",
            "Meat",
            "Sauce"
        }, "Meatballs");
    }

    // Recipe 3 - Salad
    public void ChooseRecipe3()
    {
        SelectRecipe(new string[]
        {
            "Cheese",
            "Vegetables",
            "Oil"
        }, "Salad");
    }

    // Select or change recipe
    void SelectRecipe(string[] ingredients, string dishName)
    {
        selectedIngredients = ingredients;
        selectedDishName = dishName;

        InkDialogueManager dialogue =
            FindObjectOfType<InkDialogueManager>();

        if (dialogue != null)
        {
            dialogue.CloseDialogue();
        }

        Debug.Log(dishName + " selected!");

        // Start timer only once
        if (!timerStarted)
        {
            if (satisfactionTimer != null)
            {
                satisfactionTimer.StartTimer();
                timerStarted = true;
            }
            else
            {
                Debug.LogError("Satisfaction Timer is missing!");
            }
        }
    }

    // Check if ingredient belongs to selected recipe
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
