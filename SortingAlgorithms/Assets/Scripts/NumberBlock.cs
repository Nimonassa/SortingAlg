using TMPro;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int value;


    public void SetValue(int number)
    {
        value = number;
        text.text = number.ToString();
    }
}