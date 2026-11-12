
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServingCounter : MonoBehaviour
{
    public Transform servingPoint;

    [Header("Served Dish Prefabs")]
    public GameObject fishPrefab;
    public GameObject meatballsPrefab;
    public GameObject saladPrefab;

    public SatisfactionTimer satisfactionTimer;
    public GameObject retryButton;

    void Start()
    {
        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }
    }

    public void ServeDish(string cookingResult, string dishName)
    {
        if (servingPoint == null)
        {
            Debug.LogError("Serving Point is missing!");
            return;
        }

        // Select the correct 3D dish
        GameObject selectedDishPrefab = null;

        if (dishName == "Fish")
        {
            selectedDishPrefab = fishPrefab;
        }
        else if (dishName == "Meatballs")
        {
            selectedDishPrefab = meatballsPrefab;
        }
        else if (dishName == "Salad")
        {
            selectedDishPrefab = saladPrefab;
        }

        if (selectedDishPrefab == null)
        {
            Debug.LogError("No served dish prefab assigned for: " + dishName);
            return;
        }

        // Spawn the correct dish
        GameObject dish = Instantiate(
            selectedDishPrefab,
            servingPoint.position,
            servingPoint.rotation
        );

        // Play the cooking result VFX
        ServedDishVFX vfx = dish.GetComponent<ServedDishVFX>();

        if (vfx != null)
        {
            vfx.PlayEffect(cookingResult);
        }

        // Apply Athena's preference and stop timer
        if (satisfactionTimer != null)
        {
            satisfactionTimer.ApplyAthenaPreference(dishName);
            satisfactionTimer.StopTimer();
        }

        // Show Retry Button
        if (retryButton != null)
        {
            retryButton.SetActive(true);
        }

        Debug.Log("Dish served: " + dishName);
        Debug.Log("Cooking result: " + cookingResult);
    }

    // Restart the current scene
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
