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

        for(int i = 0; i < numbers.Count; i++)
        {

            GameObject block = Instantiate(
                numberBlockPrefab,
                startPosition.position + new Vector3(i * spacing,0,0),
                Quaternion.identity
            );


            block.GetComponent<NumberBlock>()
                .SetValue(numbers[i]);


            blocks.Add(block);

        }

    }



    public void StartSorting()
    {
        StartCoroutine(InsertionSortSteps());
    }





    IEnumerator InsertionSortSteps()
    {

        for(int i = 1; i < numbers.Count; i++)
        {

            int current = numbers[i];


            int j = i - 1;



            Debug.Log("Selected: " + current);



            while(j >= 0 && current < numbers[j])
            {

                Debug.Log(
                    current +
                    " moves before " +
                    numbers[j]
                );


                numbers[j+1] = numbers[j];

                blocks[j+1]
                    .transform.position =
                    blocks[j].transform.position;



                j--;


                yield return new WaitForSeconds(1);

            }



            numbers[j+1] = current;



            // Move visual block
            blocks[j+1]
                .GetComponent<NumberBlock>()
                .SetValue(current);



            Vector3 target =
                startPosition.position +
                new Vector3(j * spacing,0,0);



            yield return MoveBlock(
                blocks[j+1],
                target
            );

        }


        Debug.Log("Finished");

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