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
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();

            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);

                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
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
        // Create the 2D inventory item
        GameObject item = new GameObject(worldItem.itemName);

        // Put it inside the slot
        item.transform.SetParent(slot.transform, false);

        // Add UI Image
        Image image = item.AddComponent<Image>();

        // Use the icon from the 3D item
        image.sprite = worldItem.inventoryIcon;
        image.preserveAspect = true;

        // Add CanvasGroup for dragging
        item.AddComponent<CanvasGroup>();

        // Add your existing drag script
        ItemDragHandler dragHandler = item.AddComponent<ItemDragHandler>();

        // Tell the drag script which 3D item it belongs to
        dragHandler.worldItem = worldItem;

        // Tell the slot that it contains this item
        slot.currentItem = item;

        // Make the icon fill the slot
        RectTransform rect = item.GetComponent<RectTransform>();

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        // Hide the original 3D item
        worldItem.gameObject.SetActive(false);
    }
}