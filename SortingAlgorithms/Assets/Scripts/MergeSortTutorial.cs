using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSortTutorial : MonoBehaviour
{
    [Header("UI Number Panels")]
    public List<RectTransform> panels = new List<RectTransform>();

    [Header("Settings")]
    public float separationDistance = 200f;
    public float moveDuration = 1f;
    public float startDelay = 2f;

    private Vector2[] startPositions;


    void Start()
    {
        // Store starting positions
        startPositions = new Vector2[panels.Count];

        for (int i = 0; i < panels.Count; i++)
        {
            startPositions[i] = panels[i].anchoredPosition;
        }

        // Wait before moving
        StartCoroutine(StartTutorial());
    }


    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(startDelay);

        StartCoroutine(SplitGroups());
    }


    IEnumerator SplitGroups()
    {
        Vector2[] targetPositions = new Vector2[panels.Count];


        for (int i = 0; i < panels.Count; i++)
        {
            targetPositions[i] = startPositions[i];

            // First 3 move left
            if (i < 3)
            {
                targetPositions[i] += Vector2.left * separationDistance;
            }

            // Last 3 move right
            else
            {
                targetPositions[i] += Vector2.right * separationDistance;
            }
        }


        float timer = 0;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;

            float t = timer / moveDuration;

            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].anchoredPosition =
                    Vector2.Lerp(
                        startPositions[i],
                        targetPositions[i],
                        t
                    );
            }

            yield return null;
        }


        // Snap to final positions
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].anchoredPosition = targetPositions[i];
        }
    }
}