
using UnityEngine;

public class FinishedDishPickup : MonoBehaviour
{
    public string cookingResult;

    private bool collected = false;

    void OnMouseDown()
    {
        if (collected) return;

        WorldItem worldItem = GetComponent<WorldItem>();

        if (worldItem == null)
        {
            Debug.LogError("Finished dish has no WorldItem!");
            return;
        }

        InvetoryController inventory =
            FindFirstObjectByType<InvetoryController>();

        if (inventory == null)
        {
            Debug.LogError("Inventory not found!");
            return;
        }

        inventory.AddItem(worldItem);

        collected = true;

        Destroy(gameObject);
    }
}
