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

    [Header("Merge Output Slots")]
public List<RectTransform> outputSlots = new List<RectTransform>();

[Header("Output Slots")]
public OutputSlotsController outputSlotsController;


    [Header("Settings")]
    public float firstSplitDistance = 200f;
    public float secondSplitDistance = 100f;

    public float moveDuration = 1f;
    public float startDelay = 2f;


    private Vector2[] startPositions;

[Header("Next Screen")]
public GameObject practiceScene;

[Header("Tutorial Root")]
public GameObject tutorialScene;

private bool mergeSortFinished = false;

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
    "Now we begin the merge process.";


yield return new WaitForSeconds(2f);


tutorialText.text =
    "Merge the separated numbers into pairs.";

yield return new WaitForSeconds(2f);


tutorialText.text =
    "Compare each pair and sort them. The smaller number is placed first.";

yield return StartCoroutine(FourthStepCreatePairs());

tutorialText.text = "Now we merge each half into sorted order.";

yield return StartCoroutine(FifthStepMergeTriplets());


yield return new WaitForSeconds(2f);

    tutorialText.text =
        "Both halves are now sorted.";

yield return StartCoroutine(SeventhStepFinalMerge());

yield return new WaitForSeconds(2f);

mergeSortFinished = true;
    }




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

IEnumerator FifthStepMergeTriplets()
{
    tutorialText.text = "Merge [3,8] with [1].";

    yield return new WaitForSeconds(0.5f);

    // 3 8 1 -> 3 1 8
    yield return StartCoroutine(Swap(1, 2));

    yield return new WaitForSeconds(0.25f);

    // 3 1 8 -> 1 3 8
    yield return StartCoroutine(Swap(0, 1));

    tutorialText.text = "The left half is now sorted.";

    yield return new WaitForSeconds(3.5f);

    tutorialText.text = "The right half is already sorted and in the correct order.";


}

IEnumerator SeventhStepFinalMerge()
{

    outputSlotsController.ShowSlots();

tutorialText.text =
    "We will now build a new sorted array.";

    yield return new WaitForSeconds(3f);

    tutorialText.text =
        "And merge the two halves into this final sorted array.";

    yield return new WaitForSeconds(4f);


    tutorialText.text =
        "Compare 1 and 2.";

    yield return new WaitForSeconds(2f);

    tutorialText.text =
        "1 is smaller, so it is copied first.";

    yield return StartCoroutine(MovePanelToSlot(panels[0], outputSlots[0]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "Now compare 3 and 2.";

    yield return new WaitForSeconds(2f);

    tutorialText.text =
        "2 is smaller.";

    yield return StartCoroutine(MovePanelToSlot(panels[3], outputSlots[1]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "Compare 3 and 6.";

    yield return new WaitForSeconds(2f);

    tutorialText.text =
        "3 is smaller.";

    yield return StartCoroutine(MovePanelToSlot(panels[1], outputSlots[2]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "Compare 8 and 6.";

    yield return new WaitForSeconds(2f);

    tutorialText.text =
        "6 is smaller.";

    yield return StartCoroutine(MovePanelToSlot(panels[4], outputSlots[3]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "Compare 8 and 9.";

    yield return new WaitForSeconds(2f);

    tutorialText.text =
        "8 is smaller.";

    yield return StartCoroutine(MovePanelToSlot(panels[2], outputSlots[4]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "Only 9 remains.";

    yield return StartCoroutine(MovePanelToSlot(panels[5], outputSlots[5]));

    yield return new WaitForSeconds(2f);


    tutorialText.text =
        "The array is now completely sorted!";

 yield return new WaitForSeconds(2f);

 MenuManager manager = FindFirstObjectByType<MenuManager>();

if (manager != null)
{
    manager.MergeSortFinished();
}
else
{
    Debug.LogError("MenuManager not found in the scene.");
}
 
}

    IEnumerator Swap(int indexA, int indexB)
{
    RectTransform panelA = panels[indexA];
    RectTransform panelB = panels[indexB];

    Vector2 startA = panelA.anchoredPosition;
    Vector2 startB = panelB.anchoredPosition;

    float timer = 0f;

    while (timer < moveDuration)
    {
        timer += Time.deltaTime;
        float t = timer / moveDuration;

        panelA.anchoredPosition = Vector2.Lerp(startA, startB, t);
        panelB.anchoredPosition = Vector2.Lerp(startB, startA, t);

        yield return null;
    }

    panelA.anchoredPosition = startB;
    panelB.anchoredPosition = startA;

    // Update the list so future steps use the new order
    panels[indexA] = panelB;
    panels[indexB] = panelA;

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

    IEnumerator MovePanelToSlot(RectTransform panel, RectTransform slot)
{
    Vector2 start = panel.anchoredPosition;
    Vector2 end = slot.anchoredPosition;

    float timer = 0f;

    while (timer < moveDuration)
    {
        timer += Time.deltaTime;

        float t = timer / moveDuration;

        panel.anchoredPosition = Vector2.Lerp(start, end, t);

        yield return null;
    }

    panel.anchoredPosition = end;
}


}