using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject pageToOpen;
    public GameObject buttonToOpen;
    public GameObject[] pagesToClose;
    public GameObject[] buttonsToClose;

    public void ButtonClicked()
    {
        // Close all other pages
        foreach (GameObject page in pagesToClose)
        {
            page.SetActive(false);
        }

        foreach (GameObject page in buttonsToClose)
        {
            page.SetActive(false);
        }

        // Open this button's page
        pageToOpen.SetActive(true);
        buttonToOpen.SetActive(true);
    }
}