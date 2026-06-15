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



    public IEnumerator MoveTo(Vector3 target, float duration)
    {

        Vector3 start = transform.position;

        float time = 0;


        while(time < duration)
        {

            transform.position =
            Vector3.Lerp(start,target,time/duration);


            time += Time.deltaTime;


            yield return null;

        }


        transform.position = target;

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