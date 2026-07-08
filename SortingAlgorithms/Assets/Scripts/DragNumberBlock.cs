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
        Debug.Log("Began dragging: " + numberBlock.value);

        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging: " + numberBlock.value);
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
{
    Debug.Log("Ended dragging: " + numberBlock.value);

    canvasGroup.blocksRaycasts = true;

    DropSlot closestSlot = null;
    float closestDistance = 80f;

    foreach (DropSlot slot in FindObjectsByType<DropSlot>(FindObjectsSortMode.None))
    {
        float distance = Vector2.Distance(
            rectTransform.position,
            slot.GetComponent<RectTransform>().position);

        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestSlot = slot;
        }
    }

    if (closestSlot != null)
    {

        Debug.Log("Snapping to: " + closestSlot.name);
        numberBlock.SetSlot(closestSlot);
    }
    else
    {
        transform.SetParent(startParent, false);
        rectTransform.anchoredPosition = startPosition;
    }
}
}