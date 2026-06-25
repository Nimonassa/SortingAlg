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


    public void SetValue(int number)
    {
        value = number;

        if(numberText != null)
        {
            numberText.text = number.ToString();
        }
    }



    // Move the panel smoothly
    public IEnumerator MoveTo(Vector2 target, float speed)
    {
        RectTransform rect =
            GetComponent<RectTransform>();

        Vector2 start =
            rect.anchoredPosition;


        float time = 0;


        while(time < 1)
        {
            time += Time.deltaTime * speed;


            rect.anchoredPosition =
                Vector2.Lerp(
                    start,
                    target,
                    time
                );


            yield return null;
        }


        rect.anchoredPosition = target;
    }




    // Highlight when comparing
    public void Highlight()
    {
        if(blockImage != null)
        {
            blockImage.color = highlightColor;
        }
    }



    // Normal state
    public void ResetColor()
    {
        if(blockImage != null)
        {
            blockImage.color = normalColor;
        }
    }



    // Mark as sorted
    public void SetSorted()
    {
        if(blockImage != null)
        {
            blockImage.color = sortedColor;
        }
    }
}