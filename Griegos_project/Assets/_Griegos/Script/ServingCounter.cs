
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServingCounter : MonoBehaviour
{
    public Transform servingPoint;
    public GameObject servedDishPrefab;
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
        if (servingPoint == null || servedDishPrefab == null)
        {
            Debug.LogError("Serving Point or Served Dish Prefab is missing!");
            return;
        }

        GameObject dish = Instantiate(
            servedDishPrefab,
            servingPoint.position,
            servingPoint.rotation
        );

        ServedDishVFX vfx = dish.GetComponent<ServedDishVFX>();

        if (vfx != null)
        {
            vfx.PlayEffect(cookingResult);
        }

        if (satisfactionTimer != null)
        {
            satisfactionTimer.ApplyAthenaPreference(dishName);
            satisfactionTimer.StopTimer();
        }

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
