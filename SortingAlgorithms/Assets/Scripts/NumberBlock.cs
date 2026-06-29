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

    public bool IsMoving { get; private set; } = false;

    public void SetValue(int number)
    {
        value = number;
        if (numberText != null) numberText.text = number.ToString();
    }

    // Move the panel smoothly using anchoredPosition; speed is units per second.
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
        Debug.Log($"{name}: MoveTo start {start} -> target {targetAnchoredPos} at speed {speed}");

        while (Vector2.Distance(rect.anchoredPosition, targetAnchoredPos) > 0.5f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, targetAnchoredPos, speed * Time.deltaTime);
            yield return null;
        }

        rect.anchoredPosition = targetAnchoredPos;
        IsMoving = false;
        Debug.Log($"{name}: MoveTo finished at {rect.anchoredPosition}");
    }

    public void Highlight()
    {
        if (blockImage != null) blockImage.color = highlightColor;
    }

    public void ResetColor()
    {
        if (blockImage != null) blockImage.color = normalColor;
    }

    public void SetSorted()
    {
        if (blockImage != null) blockImage.color = sortedColor;
    }
}
