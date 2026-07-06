using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI numberText;
    public Image blockImage;

    [Header("Value")]
    public int value;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public Color sortedColor = Color.green;

    // The drop slot this number is currently occupying.
    [HideInInspector]
    public DropSlot currentSlot;

    public bool IsMoving { get; private set; } = false;

    public void SetValue(int number)
    {
        value = number;

        if (numberText != null)
            numberText.text = number.ToString();
    }

    // Moves the UI panel smoothly to a target anchored position.
    public IEnumerator MoveTo(Vector2 targetAnchoredPos, float speed)
    {
        RectTransform rect = GetComponent<RectTransform>();

        if (rect == null)
        {
            Debug.LogWarning($"{name}: No RectTransform found.");
            yield break;
        }

        Vector2 start = rect.anchoredPosition;

        if (Vector2.Distance(start, targetAnchoredPos) < 0.01f)
        {
            rect.anchoredPosition = targetAnchoredPos;
            yield break;
        }

        if (speed <= 0f)
        {
            rect.anchoredPosition = targetAnchoredPos;
            yield break;
        }

        IsMoving = true;

        Debug.Log($"{name}: MoveTo start {start} -> target {targetAnchoredPos}");

        while (Vector2.Distance(rect.anchoredPosition, targetAnchoredPos) > 0.5f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(
                rect.anchoredPosition,
                targetAnchoredPos,
                speed * Time.deltaTime);

            yield return null;
        }

        rect.anchoredPosition = targetAnchoredPos;
        IsMoving = false;

        Debug.Log($"{name}: MoveTo finished.");
    }

    public void Highlight()
    {
        if (blockImage != null)
            blockImage.color = highlightColor;
    }

    public void ResetColor()
    {
        if (blockImage != null)
            blockImage.color = normalColor;
    }

    public void SetSorted()
    {
        if (blockImage != null)
            blockImage.color = sortedColor;
    }


    public void SetSlot(DropSlot slot)
    {
        // Remove ourselves from the previous slot.
        if (currentSlot != null)
            currentSlot.currentBlock = null;

        currentSlot = slot;

        if (slot != null)
            slot.currentBlock = this;
    }
}