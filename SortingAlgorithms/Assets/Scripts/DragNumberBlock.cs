using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNumberBlock : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("Palette")]
    public GameObject draggablePrefab;

    private Canvas canvas;

    // The clone currently being dragged
    private GameObject draggingClone;
    private RectTransform cloneRect;
    private CanvasGroup cloneCanvasGroup;
    private NumberBlock cloneNumberBlock;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create a copy
        draggingClone = Instantiate(
            draggablePrefab,
            canvas.transform);

        draggingClone.transform.SetAsLastSibling();

        cloneRect = draggingClone.GetComponent<RectTransform>();
        cloneCanvasGroup = draggingClone.GetComponent<CanvasGroup>();
        cloneNumberBlock = draggingClone.GetComponent<NumberBlock>();

        // Copy the number value
        NumberBlock original = GetComponent<NumberBlock>();
        cloneNumberBlock.SetValue(original.value);

        // Start exactly where the original is
        cloneRect.position = GetComponent<RectTransform>().position;

        cloneCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cloneRect == null)
            return;

        cloneRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingClone == null)
            return;

        cloneCanvasGroup.blocksRaycasts = true;

        DropSlot closestSlot = FindClosestSlot();

        if (closestSlot != null)
        {
            cloneNumberBlock.SetSlot(closestSlot);

            Debug.Log("Placed " + cloneNumberBlock.value);
        }
        else
        {
            Destroy(draggingClone);
        }

        draggingClone = null;
    }

    private DropSlot FindClosestSlot()
    {
        DropSlot[] slots =
            FindObjectsByType<DropSlot>(FindObjectsSortMode.None);

        DropSlot closest = null;
        float minDistance = 80f;

        foreach (DropSlot slot in slots)
        {
            float distance = Vector2.Distance(
                cloneRect.position,
                slot.GetComponent<RectTransform>().position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = slot;
            }
        }

        return closest;
    }
}