using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MergeSortTutorial : MonoBehaviour
{
    [Header("Blocks")]
    public List<GameObject> blocks;


    [Header("Tutorial Text")]
    public TextMeshProUGUI tutorialText;


    public float moveSpeed = 2f;



    void Start()
    {
        StartCoroutine(StartMergeSort());
    }



    IEnumerator StartMergeSort()
    {
        tutorialText.text =
            "Merge Sort starts by dividing the list into smaller groups.\n\n" +
            "First, we split the array into two halves.";

        yield return new WaitForSeconds(2);


        yield return StartCoroutine(SplitFirstStage());


        tutorialText.text =
            "The first half contains:\n\n" +
            "8   3   1\n\n" +
            "The second half contains:\n\n" +
            "6   2   9";


    }




    IEnumerator SplitFirstStage()
    {

        float leftSpacing = 100f;
        float rightSpacing = 100f;

        float leftStart = -250f;
        float rightStart = 100f;



        // Move first 3 blocks left group

        for(int i = 0; i < 3; i++)
        {

            Vector2 target =
                new Vector2(
                    leftStart + i * leftSpacing,
                    0
                );


            StartCoroutine(
                blocks[i]
                .GetComponent<NumberBlock>()
                .MoveTo(target, moveSpeed)
            );

        }



        // Move second 3 blocks right group

        for(int i = 3; i < 6; i++)
        {

            Vector2 target =
                new Vector2(
                    rightStart + (i-3) * rightSpacing,
                    0
                );


            StartCoroutine(
                blocks[i]
                .GetComponent<NumberBlock>()
                .MoveTo(target, moveSpeed)
            );

        }



        yield return new WaitForSeconds(moveSpeed);

    }

}