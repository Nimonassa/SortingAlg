using System.Collections.Generic;
using UnityEngine;


public class MergeSortSpawner : MonoBehaviour
{

    public GameObject numberPrefab;

    public MergeSortTutorial tutorial;


    public int[] numbers =
    {
        8,3,5,4,7,6,1,2
    };


    public List<GameObject> blocks = new List<GameObject>();


    private bool hasSpawned = false;



    public void SpawnBlocks()
    {

        if(hasSpawned)
            return;


        hasSpawned = true;


        CreateBlocks();


        tutorial.blocks = blocks;


        tutorial.PositionCamera(blocks);


        tutorial.StartSorting();

    }




    void CreateBlocks()
    {

        for(int i = 0; i < numbers.Length; i++)
        {

            GameObject block = Instantiate(numberPrefab);


block.transform.position =
new Vector3(510+ (i * 2),388,0);

            block.GetComponent<NumberBlock>()
            .SetValue(numbers[i]);



            blocks.Add(block);

        }

    }

}