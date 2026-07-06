using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberDrag : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 startPosition;
    private Transform startParent;

    private NumberBlock numberBlock;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        numberBlock = GetComponent<NumberBlock>();

        canvas = GetComponentInParent<Canvas>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        DropSlot slot = null;

        if (eventData.pointerEnter != null)
        {
            slot = eventData.pointerEnter.GetComponent<DropSlot>();

            if (slot == null)
                slot = eventData.pointerEnter.GetComponentInParent<DropSlot>();
        }

        if (slot != null)
        {
            // Remove from previous slot
            if (numberBlock.currentSlot != null)
                numberBlock.currentSlot.currentBlock = null;

            // Assign new slot
            numberBlock.SetSlot(slot);

            // Snap into place
            transform.SetParent(slot.transform);

            RectTransform slotRect =
                slot.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Return to original position
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }
    }
}