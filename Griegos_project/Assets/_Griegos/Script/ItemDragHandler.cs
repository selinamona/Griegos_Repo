using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Save original parent

        // Keep the item inside the Canvas while dragging
        transform.SetParent(GetComponentInParent<Canvas>().transform, true);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // Semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Find the slot we're dropping onto
        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();

        // Find the original slot
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                // Slot has an item - swap items

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
        }
        else
        {
            // No slot under drop point
            // Return the item to its original slot
            transform.SetParent(originalParent);
        }

        // Center the item inside its slot
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}