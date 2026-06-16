using System.Collections.Generic;
using UnityEngine;

public class MergeSortSpawner : MonoBehaviour
{
    public GameObject numberPrefab;
    public RectTransform spawnParent;

    public int[] numbers =
    {
        8, 3, 5, 4, 7, 6, 1, 2
    };

    public List<GameObject> blocks = new List<GameObject>();

    private bool hasSpawned = false;

    /// <summary>
    /// Call this FROM your tutorial when it finishes
    /// </summary>
    public void SpawnBlocks()
    {
        if (hasSpawned)
        {
            Debug.Log("Blocks already spawned — ignoring");
            return;
        }

        hasSpawned = true;

        CreateBlocks();
    }

    private void CreateBlocks()
    {
        // clear old UI children
        foreach (Transform child in spawnParent)
        {
            Destroy(child.gameObject);
        }

        blocks.Clear();

        float spacing = 100f;

        for (int i = 0; i < numbers.Length; i++)
        {
            GameObject block = Instantiate(numberPrefab, spawnParent);

            RectTransform rt = block.GetComponent<RectTransform>();
            rt.anchoredPosition = GetCenteredPosition(i, numbers.Length, spacing);

            NumberBlock nb = block.GetComponent<NumberBlock>();
            if (nb != null)
            {
                nb.SetValue(numbers[i]);
            }

            blocks.Add(block);
        }
    }

    private Vector2 GetCenteredPosition(int index, int count, float spacing)
    {
        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        float x = startX + index * spacing;

        return new Vector2(x, 0f);
    }
}