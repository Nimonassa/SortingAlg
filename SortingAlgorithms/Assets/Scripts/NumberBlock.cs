using System.Collections;
using TMPro;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{

    public int value;

public TextMeshProUGUI text;

    public void SetValue(int number)
    {
        value = number;
        text.text = number.ToString();
    }



    public IEnumerator MoveTo(Vector2 target, float duration)
{
    RectTransform rt = GetComponent<RectTransform>();

    Vector2 start = rt.anchoredPosition;
    float t = 0;

    while (t < duration)
    {
        t += Time.deltaTime;
        rt.anchoredPosition = Vector2.Lerp(start, target, t / duration);
        yield return null;
    }

    rt.anchoredPosition = target;
}



    public void Highlight()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }



    public void ResetColor()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

}