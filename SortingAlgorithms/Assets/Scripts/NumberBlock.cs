using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberBlock : MonoBehaviour
{
    public int value;

    public TextMeshProUGUI text;

    private RectTransform rectTransform;
    private Image image;

    private Color originalColor;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        if (image != null)
            originalColor = image.color;
    }

    // ---------------- VALUE ----------------

    public void SetValue(int number)
    {
        value = number;
        text.text = number.ToString();
    }

    // ---------------- UI MOVEMENT ----------------

    public IEnumerator MoveTo(Vector2 target, float duration)
    {
        Vector2 start = rectTransform.anchoredPosition;

        float time = 0f;

        while (time < duration)
        {
            rectTransform.anchoredPosition =
                Vector2.Lerp(start, target, time / duration);

            time += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = target;
    }

    // ---------------- HIGHLIGHT ----------------

    public void Highlight()
    {
        if (image != null)
            image.color = Color.yellow;
    }

    public void ResetColor()
    {
        if (image != null)
            image.color = originalColor;
    }
}