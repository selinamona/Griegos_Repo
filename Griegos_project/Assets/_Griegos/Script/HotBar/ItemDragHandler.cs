using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    // The original 3D item this inventory item belongs to
    public WorldItem worldItem;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        // Keep the item inside the Canvas while dragging
        transform.SetParent(GetComponentInParent<Canvas>().transform, true);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CookingPot pot = hit.collider.GetComponentInParent<CookingPot>();

            if (pot != null)
            {
                if (worldItem != null)
                {
                    pot.AddIngredient(worldItem);
                    Destroy(gameObject);
                    return;
                }
            }
        }

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Find the slot we're dropping onto
        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();

        // Find the original slot
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            // Slot already contains an item
            if (dropSlot.currentItem != null)
            {
                GameObject otherItem = dropSlot.currentItem;

                // Move the other item to the original slot
                otherItem.transform.SetParent(originalSlot.transform);

                originalSlot.currentItem = otherItem;

                otherItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                // Original slot is now empty
                originalSlot.currentItem = null;
            }

            // Move dragged item into the new slot
            transform.SetParent(dropSlot.transform);

            dropSlot.currentItem = gameObject;

            // Center item inside the slot
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            // The item was dragged outside the inventory

            // Empty the original slot
            originalSlot.currentItem = null;

            // Bring the original 3D item back
            if (worldItem != null)
            {
                worldItem.gameObject.SetActive(true);
            }

            // Remove the 2D inventory item
            Destroy(gameObject);
        }
    }
}