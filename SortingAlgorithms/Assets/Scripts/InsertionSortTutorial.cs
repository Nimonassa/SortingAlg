using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InsertionSortTutorial : MonoBehaviour
{

    public GameObject numberBlockPrefab;

    public Transform startPosition;


    public List<int> numbers = new List<int>()
    {
        5,3,8,1,2
    };


    public float spacing = 2f;

    public float moveSpeed = 2f;



    private List<GameObject> blocks = new List<GameObject>();



    private void Start()
    {
        CreateBlocks();
    }



   void CreateBlocks()
{
    for (int i = 0; i < numbers.Count; i++)
    {
        GameObject block = Instantiate(
            numberBlockPrefab,
            startPosition
        );

        block.transform.localPosition = new Vector3(
            i * spacing,
            0,
            0
        );

        block.GetComponent<NumberBlock>()
            .SetValue(numbers[i]);

        blocks.Add(block);
        Debug.Log("Block " + i + " position: " + block.transform.localPosition);
    }
}



    public void StartSorting()
    {
        StartCoroutine(InsertionSortSteps());
    }





   IEnumerator InsertionSortSteps()
{
    for (int i = 1; i < numbers.Count; i++)
    {
        int current = numbers[i];

        GameObject currentBlock = blocks[i];


        Debug.Log("Selected: " + current);


        int j = i - 1;


        while (j >= 0 && current < numbers[j])
        {

            Debug.Log(
                current + 
                " moves before " + 
                numbers[j]
            );


            // Move number value
            numbers[j + 1] = numbers[j];


            // Move block visually
            blocks[j + 1] = blocks[j];


            Vector3 targetPosition =
                startPosition.position +
                new Vector3((j + 1) * spacing, 0, 0);


            yield return MoveBlock(
                blocks[j + 1],
                targetPosition
            );


            j--;


            yield return new WaitForSeconds(1);
        }



        // Insert number
        numbers[j + 1] = current;


        // Insert block reference
        blocks[j + 1] = currentBlock;



        Vector3 finalPosition =
            startPosition.position +
            new Vector3((j + 1) * spacing, 0, 0);


        yield return MoveBlock(
            currentBlock,
            finalPosition
        );


        Debug.Log(
            "Inserted " + current
        );


        yield return new WaitForSeconds(1);
    }


    Debug.Log("Sorting Complete");

}




    IEnumerator MoveBlock(GameObject obj, Vector3 target)
    {

        while(Vector3.Distance(
            obj.transform.position,
            target) > 0.01f)
        {

            obj.transform.position =
                Vector3.MoveTowards(
                    obj.transform.position,
                    target,
                    moveSpeed * Time.deltaTime
                );


            yield return null;

        }

    }

}