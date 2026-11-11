
using UnityEngine;

public class ServingCounter : MonoBehaviour
{
    public Transform servingPoint;
    public GameObject servedDishPrefab;

    public void ServeDish(string cookingResult)
    {
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

        Debug.Log("Dish served! Result: " + cookingResult);
    }
}
