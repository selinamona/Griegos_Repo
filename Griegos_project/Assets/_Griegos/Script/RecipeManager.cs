
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    public SatisfactionTimer satisfactionTimer;

    // Ingredients for the currently selected recipe
    public string[] selectedIngredients;

    // Has the satisfaction timer already started?
    private bool timerStarted = false;

    void Awake()
    {
        instance = this;
        selectedIngredients = null;
        timerStarted = false;
    }

    // Choose Recipe 1 - Fish
    public void ChooseRecipe1()
    {
        SelectRecipe(new string[]
        {
            "Fish",
            "Vegetables",
            "Potatos"
        }, "Recipe 1");
    }

    // Choose Recipe 2 - Meatballs
    public void ChooseRecipe2()
    {
        SelectRecipe(new string[]
        {
            "Vegetables",
            "Meat",
            "Sauce"
        }, "Recipe 2");
    }

    // Choose Recipe 3 - Salad
    public void ChooseRecipe3()
    {
        SelectRecipe(new string[]
        {
            "Cheese",
            "Vegetables",
            "Oil"
        }, "Recipe 3");
    }

    // Select or change the current recipe
    void SelectRecipe(string[] ingredients, string recipeName)
    {
        // Update the selected recipe
        selectedIngredients = ingredients;

        // Close dialogue
        InkDialogueManager dialogue =
            FindObjectOfType<InkDialogueManager>();

        if (dialogue != null)
        {
            dialogue.CloseDialogue();
        }

        Debug.Log(recipeName + " selected!");

        // Start satisfaction timer only once
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

    // Check if an ingredient belongs to the selected recipe
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
