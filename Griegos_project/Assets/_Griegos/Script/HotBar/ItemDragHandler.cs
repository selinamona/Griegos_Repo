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
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CookingPot pot = hit.collider.GetComponentInParent<CookingPot>();

            if (pot != null)
            {
                if (worldItem != null)
                {
                    // Ask the pot if this ingredient is correct
                    bool accepted = pot.AddIngredient(worldItem);

                    if (accepted)
                    {
                        // Correct ingredient
                        // Remove the 2D inventory item
                        Destroy(gameObject);
                    }
                    else
                    {
                        // Wrong ingredient
                        // Return the item to its original hotbar slot
                        transform.SetParent(originalParent);

                        Slot originalSlot = originalParent.GetComponent<Slot>();
                        originalSlot.currentItem = gameObject;

                        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    }

                    return;
                }
            }
        }

        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();

        // Find the original slot
        Slot originalSlot2 = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            // Slot already contains an item
            if (dropSlot.currentItem != null)
            {
                GameObject otherItem = dropSlot.currentItem;

                // Move the other item to the original slot
                otherItem.transform.SetParent(originalSlot2.transform);

                originalSlot2.currentItem = otherItem;

                otherItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                // Original slot is now empty
                originalSlot2.currentItem = null;
            }

            // Move dragged item into the new slot
            transform.SetParent(dropSlot.transform);

            dropSlot.currentItem = gameObject;

            // Center item inside the slot
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            // Empty the original slot
            originalSlot2.currentItem = null;

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