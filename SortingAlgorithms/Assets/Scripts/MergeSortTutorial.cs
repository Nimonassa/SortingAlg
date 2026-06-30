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
    public float firstSplitDistance = 200f;
    public float secondSplitDistance = 100f;

    public float moveDuration = 1f;
    public float startDelay = 2f;


    private Vector2[] startPositions;



    void Start()
    {
        startPositions = new Vector2[panels.Count];

        for (int i = 0; i < panels.Count; i++)
        {
            startPositions[i] = panels[i].anchoredPosition;
        }


        if (tutorialText != null)
        {
            tutorialText.text = "";
        }


        StartCoroutine(StartTutorial());
    }



    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(startDelay);


// Step 1: Split into two groups of three       
        tutorialText.text =
            "In the first step, we split the array into two groups of three numbers.";


        yield return StartCoroutine(FirstSplit());


        yield return new WaitForSeconds(1f);

// Step 2: Split each group into smaller groups

        tutorialText.text =
            "In the second step, each group is split again into smaller groups.";


        yield return StartCoroutine(SecondSplit());


// Step 3: Reset spacing and prepare for merging
tutorialText.text =
    "Now each number is separated.";


yield return new WaitForSeconds(1f);


yield return StartCoroutine(ThirdStepResetSpacing());


tutorialText.text =
    "Starting the merge process.";


yield return new WaitForSeconds(1f);


tutorialText.text =
    "Merge the separated numbers into pairs.";


yield return StartCoroutine(FourthStepCreatePairs());


tutorialText.text =
    "Compare each pair and sort them.";


    IEnumerator FirstSplit()
    {
        Vector2[] targets = new Vector2[panels.Count];


        for (int i = 0; i < panels.Count; i++)
        {
            targets[i] = startPositions[i];


            if (i < 3)
            {
                targets[i] += Vector2.left * firstSplitDistance;
            }
            else
            {
                targets[i] += Vector2.right * firstSplitDistance;
            }
        }


        yield return MovePanels(targets);
    }



    IEnumerator SecondSplit()
    {
        Vector2[] targets = new Vector2[panels.Count];


        for (int i = 0; i < panels.Count; i++)
        {
            targets[i] = panels[i].anchoredPosition;
        }


        // Split first group of three
        targets[0] += Vector2.left * secondSplitDistance;
        targets[1] += Vector2.left * secondSplitDistance;

        targets[2] += Vector2.right * secondSplitDistance;



        // Split second group of three
        targets[3] += Vector2.left * secondSplitDistance;
        targets[4] += Vector2.left * secondSplitDistance;

        targets[5] += Vector2.right * secondSplitDistance;


        yield return MovePanels(targets);
    }


    IEnumerator ThirdStepResetSpacing()
{
    Vector2[] targets = new Vector2[panels.Count];


    // Put everything back into the original equal spacing positions
    for (int i = 0; i < panels.Count; i++)
    {
        targets[i] = startPositions[i];
    }


    yield return MovePanels(targets);
}

IEnumerator FourthStepCreatePairs()
{
    Vector2[] targets = new Vector2[panels.Count];


    // Start from current positions
    for (int i = 0; i < panels.Count; i++)
    {
        targets[i] = panels[i].anchoredPosition;
    }


    // Swap 8 and 3
    targets[0] = panels[1].anchoredPosition;
    targets[1] = panels[0].anchoredPosition;


    // Swap 6 and 2
    targets[3] = panels[4].anchoredPosition;
    targets[4] = panels[3].anchoredPosition;


    yield return MovePanels(targets);


    // Update the actual list order after movement
    RectTransform temp;


    temp = panels[0];
    panels[0] = panels[1];
    panels[1] = temp;


    temp = panels[3];
    panels[3] = panels[4];
    panels[4] = temp;
}


    IEnumerator MovePanels(Vector2[] targets)
    {
        Vector2[] currentPositions = new Vector2[panels.Count];


        for (int i = 0; i < panels.Count; i++)
        {
            currentPositions[i] = panels[i].anchoredPosition;
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
                        currentPositions[i],
                        targets[i],
                        t
                    );
            }


            yield return null;
        }



        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].anchoredPosition = targets[i];
        }
    }
}}