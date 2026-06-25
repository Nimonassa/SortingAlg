using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MergeSortTutorial : MonoBehaviour
{
    [Header("Number Blocks")]
    public List<GameObject> blocks;

    [Header("Tutorial Text")]
    public TextMeshProUGUI tutorialText;

    public float moveSpeed = 1f;


    public void StartSorting()
    {
        StartCoroutine(MergeSortSequence());
    }


    IEnumerator MergeSortSequence()
    {

        tutorialText.text =
            "Merge Sort Tutorial\n\n" +
            "We start with the unsorted list:\n\n" +
            "8  3  5  4  7  6  1  2";

        yield return new WaitForSeconds(3);



        tutorialText.text =
            "Step 1: Divide\n\n" +
            "Merge Sort divides the list into smaller groups.\n\n" +
            "The array is split into two halves.";

        yield return StartCoroutine(Split());

        yield return new WaitForSeconds(2);



        tutorialText.text =
            "Step 2: Divide again\n\n" +
            "The groups are divided again and again.\n\n" +
            "This continues until every number is alone.\n\n" +
            "A single number is already sorted.";

        yield return new WaitForSeconds(3);



        tutorialText.text =
            "Step 3: Compare numbers\n\n" +
            "Now Merge Sort compares numbers in each group.\n\n" +
            "The smaller value moves before the larger value.";

        yield return StartCoroutine(Compare(0,1));



        tutorialText.text =
            "3 is smaller than 8.\n\n" +
            "So 3 moves before 8.";

        yield return StartCoroutine(Swap(0,1));



        yield return StartCoroutine(Compare(2,3));


        tutorialText.text =
            "4 is smaller than 5.\n\n" +
            "The pair is now sorted.";

        yield return StartCoroutine(Swap(2,3));



        tutorialText.text =
            "Step 4: Merge\n\n" +
            "The smaller sorted groups are merged together.\n\n" +
            "Merge Sort keeps the numbers in order while merging.";

        yield return new WaitForSeconds(3);



        tutorialText.text =
            "Final step\n\n" +
            "All groups are merged back together.\n\n" +
            "The entire list becomes sorted.";

        yield return StartCoroutine(FinalSort());



        tutorialText.text =
            "Finished!\n\n" +
            "The sorted list is:\n\n" +
            "1  2  3  4  5  6  7  8";

    }



    Vector2 GetBlockPos(GameObject obj)
    {
        return obj.GetComponent<RectTransform>().anchoredPosition;
    }


    Vector2 GetCenteredPosition(int index, int count, float spacing)
    {
        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        return new Vector2(
            startX + index * spacing,
            0f
        );
    }



    IEnumerator Split()
    {

        for(int i = 0; i < 4; i++)
        {
            Vector2 pos =
                GetBlockPos(blocks[i]) +
                new Vector2(-40f,0);


            StartCoroutine(
                blocks[i]
                .GetComponent<NumberBlock>()
                .MoveTo(pos,moveSpeed)
            );
        }



        for(int i = 4; i < 8; i++)
        {
            Vector2 pos =
                GetBlockPos(blocks[i]) +
                new Vector2(40f,0);


            StartCoroutine(
                blocks[i]
                .GetComponent<NumberBlock>()
                .MoveTo(pos,moveSpeed)
            );
        }


        yield return new WaitForSeconds(moveSpeed);
    }





    IEnumerator Compare(int a,int b)
    {

        blocks[a]
        .GetComponent<NumberBlock>()
        .Highlight();


        blocks[b]
        .GetComponent<NumberBlock>()
        .Highlight();


        yield return new WaitForSeconds(2);


        blocks[a]
        .GetComponent<NumberBlock>()
        .ResetColor();


        blocks[b]
        .GetComponent<NumberBlock>()
        .ResetColor();

    }





    IEnumerator Swap(int a,int b)
    {

        Vector2 posA = GetBlockPos(blocks[a]);

        Vector2 posB = GetBlockPos(blocks[b]);



        StartCoroutine(
            blocks[a]
            .GetComponent<NumberBlock>()
            .MoveTo(posB,moveSpeed)
        );


        StartCoroutine(
            blocks[b]
            .GetComponent<NumberBlock>()
            .MoveTo(posA,moveSpeed)
        );


        yield return new WaitForSeconds(moveSpeed);



        GameObject temp = blocks[a];

        blocks[a] = blocks[b];

        blocks[b] = temp;

    }





    IEnumerator FinalSort()
    {

        float spacing = 100f;


        blocks.Sort((a,b)=>
            a.GetComponent<NumberBlock>()
            .value.CompareTo(
            b.GetComponent<NumberBlock>()
            .value)
        );


        for(int i = 0; i < blocks.Count; i++)
        {

            Vector2 target =
                GetCenteredPosition(
                    i,
                    blocks.Count,
                    spacing
                );


            StartCoroutine(
                blocks[i]
                .GetComponent<NumberBlock>()
                .MoveTo(target,moveSpeed)
            );

        }


        yield return new WaitForSeconds(moveSpeed);

    }
}