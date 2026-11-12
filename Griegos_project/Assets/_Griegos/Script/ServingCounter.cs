
using UnityEngine;

public class ServingCounter : MonoBehaviour
{
    public Transform servingPoint;
    public GameObject servedDishPrefab;
    public SatisfactionTimer satisfactionTimer;

    public GameObject retryButton;

    void Start()
    {
        // Hide retry button until a dish is served
        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }
    }

    public void ServeDish(string cookingResult, string dishName)
    {
        Debug.Log("ServeDish was called!");

        if (servingPoint == null || servedDishPrefab == null)
        {
            Debug.LogError("Serving Point or Served Dish Prefab is missing!");
            return;
        }

        // Spawn the served dish
        GameObject dish = Instantiate(
            servedDishPrefab,
            servingPoint.position,
            servingPoint.rotation
        );

        // Play cooking result VFX
        ServedDishVFX vfx = dish.GetComponent<ServedDishVFX>();

        if (vfx != null)
        {
            vfx.PlayEffect(cookingResult);
        }

        // Apply Athena's preference and stop the timer
        if (satisfactionTimer != null)
        {
            satisfactionTimer.ApplyAthenaPreference(dishName);
            satisfactionTimer.StopTimer();
        }

        // Show retry button
        if (retryButton != null)
        {
            retryButton.SetActive(true);
        }

        Debug.Log("Dish served: " + dishName);
        Debug.Log("Cooking result: " + cookingResult);
    }
}
