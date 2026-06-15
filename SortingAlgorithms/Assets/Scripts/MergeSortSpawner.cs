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



    public List<GameObject> blocks =
    new List<GameObject>();



void Start()
{

    CreateBlocks();


    Debug.Log("Blocks created: " + blocks.Count);


    tutorial.blocks = blocks;

}

  void CreateBlocks()
{
    for (int i = 0; i < numbers.Length; i++)
    {
        GameObject block = Instantiate(numberPrefab);

        block.transform.SetParent(transform, false);

        block.transform.localPosition = new Vector3(i * 2, 0, 0);

        block.GetComponent<NumberBlock>().SetValue(numbers[i]);

        blocks.Add(block);
    }
}

}