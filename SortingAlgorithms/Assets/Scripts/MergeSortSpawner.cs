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
        if (hasSpawned)
            return;

        hasSpawned = true;

        CreateBlocks();

        tutorial.blocks = blocks;

        tutorial.PositionCamera(blocks);

        tutorial.StartSorting();
    }

    void CreateBlocks()
    {
        float spacing = 2f;

        for (int i = 0; i < numbers.Length; i++)
        {
            GameObject block = Instantiate(numberPrefab);

            Vector3 pos = GetCenteredPosition(i, numbers.Length, spacing);

            block.transform.position = pos;

            block.GetComponent<NumberBlock>()
                .SetValue(numbers[i]);

            blocks.Add(block);
        }
    }

    Vector3 GetCenteredPosition(int index, int count, float spacing)
    {
        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        float x = startX + index * spacing;

        return new Vector3(x, 0f, 0f);
    }
}