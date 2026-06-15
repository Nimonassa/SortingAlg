using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MergeSortTutorial : MonoBehaviour
{

public List<GameObject> blocks = new List<GameObject>();


    public TextMeshProUGUI tutorialText;



    public float moveSpeed = 1f;



public void StartSorting()
{

    if(blocks.Count == 0)
    {
        Debug.LogError("No blocks assigned to MergeSortTutorial!");
        return;
    }


    StartCoroutine(MergeSortSequence());

}




    IEnumerator MergeSortSequence()
    {


        tutorialText.text =
        "Merge sort divides the list into smaller groups.";


        yield return new WaitForSeconds(2);



        yield return StartCoroutine(Split());



        tutorialText.text =
        "Now the groups keep splitting.";


        yield return new WaitForSeconds(2);



        yield return StartCoroutine(Compare(0,1));



        tutorialText.text =
        "3 is smaller than 8, so it moves first.";


        yield return StartCoroutine(Swap(0,1));



        yield return StartCoroutine(Compare(2,3));


        yield return StartCoroutine(Swap(2,3));



        tutorialText.text =
        "The groups are merged back together in order.";


        yield return new WaitForSeconds(2);



        yield return StartCoroutine(FinalSort());



        tutorialText.text =
        "Finished! The list is sorted.";

    }





    IEnumerator Split()
    {


        for(int i=0;i<4;i++)
        {

            Vector3 pos =
            blocks[i].transform.position +
            new Vector3(-2,0,0);


            StartCoroutine(
            blocks[i]
            .GetComponent<NumberBlock>()
            .MoveTo(pos,moveSpeed)
            );

        }



        for(int i=4;i<8;i++)
        {

            Vector3 pos =
            blocks[i].transform.position +
            new Vector3(2,0,0);


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


        Vector3 posA =
        blocks[a].transform.position;


        Vector3 posB =
        blocks[b].transform.position;



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



        GameObject temp =
        blocks[a];


        blocks[a] =
        blocks[b];


        blocks[b] =
        temp;


    }





    IEnumerator FinalSort()
    {

        blocks.Sort(
        (a,b)=>
        a.GetComponent<NumberBlock>()
        .value
        .CompareTo(
        b.GetComponent<NumberBlock>()
        .value)
        );



        for(int i=0;i<blocks.Count;i++)
        {

            Vector3 target =
            new Vector3(i*2,0,0);



            StartCoroutine(
            blocks[i]
            .GetComponent<NumberBlock>()
            .MoveTo(target,moveSpeed)
            );


        }


        yield return new WaitForSeconds(moveSpeed);

    }

}