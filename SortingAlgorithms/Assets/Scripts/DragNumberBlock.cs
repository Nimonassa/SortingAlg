using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(NumberBlock))]
public class DragNumberBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public PracticeMerge tutorial;

    [Header("Snapping")]
    public float snapDistance = 75f;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private NumberBlock numberBlock;

    private Transform originalParent;
    private Vector2 originalAnchoredPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        numberBlock = GetComponent<NumberBlock>();

        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalAnchoredPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;

        // Bring to the front while dragging
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        DropSlot closestSlot = FindClosestSlot();

        if (closestSlot != null)
        {
            // Free previous slot
            if (numberBlock.currentSlot != null)
                numberBlock.currentSlot.currentBlock = null;

            // If another block is already here, don't snap
            if (closestSlot.currentBlock == null)
            {
                closestSlot.currentBlock = numberBlock;
                numberBlock.currentSlot = closestSlot;

                transform.SetParent(closestSlot.transform, false);
                rectTransform.anchoredPosition = Vector2.zero;

                if (tutorial != null)
                    tutorial.CheckStep();

                return;
            }
        }

        // Return to previous location
        transform.SetParent(originalParent, false);
        rectTransform.anchoredPosition = originalAnchoredPosition;
    }

    private DropSlot FindClosestSlot()
    {
        DropSlot[] slots = FindObjectsOfType<DropSlot>();

        DropSlot closest = null;
        float closestDistance = snapDistance;

        foreach (DropSlot slot in slots)
        {
            float distance = Vector2.Distance(
                rectTransform.position,
                slot.GetComponent<RectTransform>().position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = slot;
            }
        }

        return closest;
    }
}