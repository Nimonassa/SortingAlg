using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MergeSortTutorial : MonoBehaviour
{
    [Header("UI Number Panels")]
    public List<RectTransform> panels = new List<RectTransform>();

    [Header("Tutorial Text")]
    public TextMeshProUGUI tutorialText;


    [Header("Settings")]
    public float separationDistance = 200f;
    public float moveDuration = 1f;
    public float startDelay = 7f;


    private Vector2[] startPositions;


    void Start()
    {
        // Save original positions
        startPositions = new Vector2[panels.Count];

        for (int i = 0; i < panels.Count; i++)
        {
            startPositions[i] = panels[i].anchoredPosition;
        }


        // Hide text at the beginning
        if (tutorialText != null)
        {
            tutorialText.text = "";
        }


        StartCoroutine(StartTutorial());
    }



    IEnumerator StartTutorial()
    {
        // Wait before starting movement
        yield return new WaitForSeconds(startDelay);


        // Text starts exactly when panels start moving
        if (tutorialText != null)
        {
            tutorialText.text =
                "In the first step, we split the array into two groups of three numbers.";
        }


        yield return StartCoroutine(SplitGroups());


        if (tutorialText != null)
        {
            tutorialText.text =
                "In the second step, we sort each group separately.";
        }
    }



    IEnumerator SplitGroups()
    {
        Vector2[] targetPositions = new Vector2[panels.Count];


        for (int i = 0; i < panels.Count; i++)
        {
            targetPositions[i] = startPositions[i];


            // First three move left
            if (i < 3)
            {
                targetPositions[i] += Vector2.left * separationDistance;
            }

            // Last three move right
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


        // Ensure final positions
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].anchoredPosition = targetPositions[i];
        }
    }
}