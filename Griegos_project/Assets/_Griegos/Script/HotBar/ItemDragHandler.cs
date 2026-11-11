
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public WorldItem worldItem;

    // Finished dish information
    public bool isFinishedDish = false;
    public string cookingResult;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

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

        Slot originalSlot = originalParent.GetComponent<Slot>();

        // Check if dropped on a world object
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // SERVING COUNTER
            ServingCounter counter =
                hit.collider.GetComponentInParent<ServingCounter>();

            if (counter != null && isFinishedDish)
            {
                originalSlot.currentItem = null;

                counter.ServeDish(cookingResult);

                Destroy(gameObject);
                return;
            }

            // COOKING POT
            CookingPot pot =
                hit.collider.GetComponentInParent<CookingPot>();

            if (pot != null && worldItem != null && !isFinishedDish)
            {
                bool accepted = pot.AddIngredient(worldItem);

                if (accepted)
                {
                    Destroy(gameObject);
                }
                else
                {
                    ReturnToSlot(originalSlot);
                }

                return;
            }
        }

        // HOTBAR SLOT SWAPPING
        Slot dropSlot =
            eventData.pointerEnter?.GetComponentInParent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                GameObject otherItem = dropSlot.currentItem;

                otherItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = otherItem;

                otherItem.GetComponent<RectTransform>().anchoredPosition =
                    Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;

            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            // Finished dishes return to their slot if not served
            if (isFinishedDish)
            {
                ReturnToSlot(originalSlot);
                return;
            }

            // ORIGINAL INGREDIENT BEHAVIOR
            originalSlot.currentItem = null;

            if (worldItem != null)
                worldItem.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }

    void ReturnToSlot(Slot slot)
    {
        transform.SetParent(slot.transform);
        slot.currentItem = gameObject;

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
