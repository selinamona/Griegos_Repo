using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour
{
    public Camera playerCamera;
    public InvetoryController inventoryController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Don't pick up items when clicking on UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                WorldItem worldItem = hit.collider.GetComponent<WorldItem>();

                if (worldItem != null)
                {
                    inventoryController.AddItem(worldItem);

                    // Doe not destroy 3D item
                    // The InventoryController will hide it instead.
                }
            }
        }
    }
}