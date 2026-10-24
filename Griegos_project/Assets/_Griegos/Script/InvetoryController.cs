using UnityEngine;
using UnityEngine.UI;

public class InvetoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
    }

    public void AddItem(WorldItem worldItem)
    {
        // Find an empty slot
        Slot[] slots = inventoryPanel.GetComponentsInChildren<Slot>();

        foreach (Slot slot in slots)
        {
            if (slot.currentItem == null)
            {
                AddItemToSlot(worldItem, slot);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }

void AddItemToSlot(WorldItem worldItem, Slot slot)
    {
        // Create the 2D inventory item from the prefab
        GameObject item = Instantiate(worldItem.inventoryPrefab, slot.transform);

        // Make sure it is positioned correctly
        RectTransform rect = item.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;

        // Find the drag handler that already exists on the prefab
        ItemDragHandler dragHandler = item.GetComponent<ItemDragHandler>();

        // Tell the drag handler which 3D item this belongs to
        if (dragHandler != null)
        {
            dragHandler.worldItem = worldItem;
        }

        // Tell the slot that it contains this item
        slot.currentItem = item;

        // Hide the original 3D item
        worldItem.gameObject.SetActive(false);
    }
}