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

    [Header("Camera")]
    public Camera mainCamera;

    public float moveSpeed = 1f;

    public void StartSorting()
    {
        StartCoroutine(MergeSortSequence());
    }

    // Keeps camera centered on array
    public void PositionCamera(List<GameObject> blocks)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Vector3 center = Vector3.zero;

        foreach (GameObject block in blocks)
        {
            center += block.transform.position;
        }

        center /= blocks.Count;

        mainCamera.transform.position = new Vector3(center.x, center.y, -15);
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);

        mainCamera.orthographic = true;
        mainCamera.orthographicSize = 5;
    }

    // ---------------- CORE SEQUENCE ----------------

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

    // ---------------- CENTERED POSITIONING ----------------

    Vector3 GetCenteredPosition(int index, int count, float spacing)
    {
        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        float x = startX + index * spacing;

        return new Vector3(x, 0f, 0f);
    }

    Vector3 GetBlockPos(GameObject obj)
    {
        return obj.transform.position;
    }

    // ---------------- SPLIT ----------------

    IEnumerator Split()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = GetBlockPos(blocks[i]) + new Vector3(-1f, 0, 0);

            StartCoroutine(blocks[i].GetComponent<NumberBlock>().MoveTo(pos, moveSpeed));
        }

        for (int i = 4; i < 8; i++)
        {
            Vector3 pos = GetBlockPos(blocks[i]) + new Vector3(1f, 0, 0);

            StartCoroutine(blocks[i].GetComponent<NumberBlock>().MoveTo(pos, moveSpeed));
        }

        yield return new WaitForSeconds(moveSpeed);
    }

    // ---------------- COMPARE ----------------

    IEnumerator Compare(int a, int b)
    {
        blocks[a].GetComponent<NumberBlock>().Highlight();
        blocks[b].GetComponent<NumberBlock>().Highlight();

        yield return new WaitForSeconds(2);

        blocks[a].GetComponent<NumberBlock>().ResetColor();
        blocks[b].GetComponent<NumberBlock>().ResetColor();
    }

    // ---------------- SWAP ----------------

    IEnumerator Swap(int a, int b)
    {
        Vector3 posA = GetBlockPos(blocks[a]);
        Vector3 posB = GetBlockPos(blocks[b]);

        StartCoroutine(blocks[a].GetComponent<NumberBlock>().MoveTo(posB, moveSpeed));
        StartCoroutine(blocks[b].GetComponent<NumberBlock>().MoveTo(posA, moveSpeed));

        yield return new WaitForSeconds(moveSpeed);

        GameObject temp = blocks[a];
        blocks[a] = blocks[b];
        blocks[b] = temp;
    }

    // ---------------- FINAL SORT (CENTERED RESET) ----------------

    IEnumerator FinalSort()
    {
        float spacing = 2f;

        blocks.Sort((a, b) =>
            a.GetComponent<NumberBlock>().value.CompareTo(
            b.GetComponent<NumberBlock>().value)
        );

        for (int i = 0; i < blocks.Count; i++)
        {
            Vector3 target = GetCenteredPosition(i, blocks.Count, spacing);

            StartCoroutine(
                blocks[i].GetComponent<NumberBlock>().MoveTo(target, moveSpeed)
            );
        }

        yield return new WaitForSeconds(moveSpeed);
    }
}