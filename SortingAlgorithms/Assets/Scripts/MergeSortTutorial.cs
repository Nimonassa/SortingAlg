using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MergeSortTutorial : MonoBehaviour
{
    [Header("Number Blocks")]
    public List<GameObject> blocks;

    [Header("Tutorial UI Text")]
    public TextMeshProUGUI tutorialText;


    public float moveSpeed = 1f;

    public void StartSorting()
    {
        StartCoroutine(MergeSortSequence());
    }

    // UI DOES NOT NEED CAMERA LOGIC

    IEnumerator MergeSortSequence()
    {
        tutorialText.text = "Merge sort divides the list into smaller groups.";
        yield return new WaitForSeconds(2);

        yield return StartCoroutine(Split());

        tutorialText.text = "Now the groups keep splitting.";
        yield return new WaitForSeconds(2);

        yield return StartCoroutine(Compare(0, 1));

        tutorialText.text = "3 is smaller than 8, so it moves first.";
        yield return StartCoroutine(Swap(0, 1));

        yield return StartCoroutine(Compare(2, 3));
        yield return StartCoroutine(Swap(2, 3));

        tutorialText.text = "The groups are merged back together in order.";
        yield return new WaitForSeconds(2);

        yield return StartCoroutine(FinalSort());

        tutorialText.text = "Finished! The list is sorted.";
    }

    // ---------------- UI MOVE HELPERS ----------------

    Vector2 GetPos(GameObject obj)
    {
        return obj.GetComponent<RectTransform>().anchoredPosition;
    }

    void SetPos(GameObject obj, Vector2 pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    

    IEnumerator Split()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 pos = GetPos(blocks[i]) + new Vector2(-40, 0);

            StartCoroutine(
                blocks[i].GetComponent<NumberBlock>()
                .MoveTo(pos, moveSpeed)
            );
        }

        for (int i = 4; i < 8; i++)
        {
            Vector2 pos = GetPos(blocks[i]) + new Vector2(40, 0);

            StartCoroutine(
                blocks[i].GetComponent<NumberBlock>()
                .MoveTo(pos, moveSpeed)
            );
        }

        yield return new WaitForSeconds(moveSpeed);
    }

    IEnumerator Compare(int a, int b)
    {
        blocks[a].GetComponent<NumberBlock>().Highlight();
        blocks[b].GetComponent<NumberBlock>().Highlight();

        yield return new WaitForSeconds(2);

        blocks[a].GetComponent<NumberBlock>().ResetColor();
        blocks[b].GetComponent<NumberBlock>().ResetColor();
    }

    IEnumerator Swap(int a, int b)
    {
        Vector2 posA = GetPos(blocks[a]);
        Vector2 posB = GetPos(blocks[b]);

        StartCoroutine(blocks[a].GetComponent<NumberBlock>().MoveTo(posB, moveSpeed));
        StartCoroutine(blocks[b].GetComponent<NumberBlock>().MoveTo(posA, moveSpeed));

        yield return new WaitForSeconds(moveSpeed);

        GameObject temp = blocks[a];
        blocks[a] = blocks[b];
        blocks[b] = temp;
    }

    IEnumerator FinalSort()
    {
        blocks.Sort((a, b) =>
            a.GetComponent<NumberBlock>().value.CompareTo(
            b.GetComponent<NumberBlock>().value)
        );

        for (int i = 0; i < blocks.Count; i++)
        {
            Vector2 target = new Vector2(i * 80, 0);

            StartCoroutine(
                blocks[i].GetComponent<NumberBlock>()
                .MoveTo(target, moveSpeed)
            );
        }

        yield return new WaitForSeconds(moveSpeed);
    }
}