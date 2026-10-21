using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public GameObject pageToOpen;
    public GameObject[] pagesToClose;

    public void ButtonClicked()
    {
        // Close all other pages
        foreach (GameObject page in pagesToClose)
        {
            page.SetActive(false);
        }

        // Open this button's page
        pageToOpen.SetActive(true);
    }
}