using System.Collections;
using TMPro;
using UnityEngine;


public class PracticeMerge : MonoBehaviour
{
    public enum TutorialStep
    {
        Divide,
        Sort,
        Merge,
        Finished
    }

    public TutorialStep currentStep;

    [Header("Tutorial")]
    public TMP_Text tutorialText;

    [Header("Slots")]
    public DropSlot[] divideSlots;
    public DropSlot[] sortSlots;
    public DropSlot[] mergeSlots;

    [Header("Feedback")]
    public TMP_Text feedbackText;
    private void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(1f);
        BeginDivideStep();

        yield return new WaitForSeconds(13f);
        BeginSortStep();

        yield return new WaitForSeconds(18f);
        BeginMergeStep();

    }

    void BeginDivideStep()
    {
        currentStep = TutorialStep.Divide;

        tutorialText.text =
            "Step 1\n\n" +
            "In row 1, Merge Sort begins by dividing the array into two halves.\n\n" +
            "Drag the first three numbers into the LEFT half without changing the order.\n" +
            "Do the same to the last three numbers by putting them into the RIGHT half.";
    }

    void BeginSortStep()
    {
        currentStep = TutorialStep.Sort;

        tutorialText.text =
            "Now in row 2, sort BOTH halves separately.\n\n" +
            "Left Half:\n8 12 27\n\n" +
            "Right Half:\n2 11 35";
    }

    void BeginMergeStep()
    {
        currentStep = TutorialStep.Merge;

        tutorialText.text =
            "In the third row, demonstrate the merge between the two sorted halves.\n\n" +
            "Compare the first number in each half and drag the smaller one into the final row like demonstrated in the inst .\n" +
            "Repeat until every number has been merged.";
    }

    void FinishTutorial()
    {
        currentStep = TutorialStep.Finished;

        tutorialText.text =
            "Congratulations!\n\n" +
            "You successfully completed Merge Sort!";
    }

    public void CheckStep()
    {
        switch (currentStep)
        {
            case TutorialStep.Divide:
                if (CheckDivide())
                    BeginSortStep();
                break;

            case TutorialStep.Sort:
                if (CheckSort())
                    BeginMergeStep();
                break;

            case TutorialStep.Merge:
                if (CheckMerge())
                    FinishTutorial();
                break;
        }
    }

    bool CheckDivide()
    {
        int[] expected =
        {
            12, 27, 8,
            35, 11, 2
        };

        if (divideSlots.Length != expected.Length)
            return false;

        for (int i = 0; i < expected.Length; i++)
        {
            if (divideSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }

    bool CheckSort()
    {
        int[] expected =
        {
            8, 12, 27,
            2, 11, 35
        };

        if (sortSlots.Length != expected.Length)
            return false;

        for (int i = 0; i < expected.Length; i++)
        {
            if (sortSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }

    bool CheckMerge()
    {
        int[] expected =
        {
            2, 8, 11, 12, 27, 35
        };

        if (mergeSlots.Length != expected.Length)
            return false;

        for (int i = 0; i < expected.Length; i++)
        {
            if (mergeSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }

    public void SubmitAnswer()
{
    bool divideCorrect = CheckDivide();
    bool sortCorrect = CheckSort();
    bool mergeCorrect = CheckMerge();

    // Check for completely empty rows first
    bool divideEmpty = IsRowEmpty(divideSlots);
    bool sortEmpty = IsRowEmpty(sortSlots);
    bool mergeEmpty = IsRowEmpty(mergeSlots);

    // Everything is correct
    if (divideCorrect && sortCorrect && mergeCorrect)
    {
        feedbackText.text = "Everything is correct!\n\n" +
                            "You successfully completed all three stages of Merge Sort!";
        return;
    }

    // Build feedback message
    string message = "";

    if (divideEmpty)
    {
        message += "Row 1 (Divide) has not been filled.\n";
    }
    else if (!divideCorrect)
    {
        message += "Row 1 (Divide) contains mistakes.\n";
    }

    if (sortEmpty)
    {
        message += "Row 2 (Sort) has not been filled.\n";
    }
    else if (!sortCorrect)
    {
        message += "Row 2 (Sort) contains mistakes.\n";
    }

    if (mergeEmpty)
    {
        message += "Row 3 (Merge) has not been filled.\n";
    }
    else if (!mergeCorrect)
    {
        message += "Row 3 (Merge) contains mistakes.\n";
    }

    feedbackText.text = message;
}

bool IsRowEmpty(DropSlot[] slots)
{
    for (int i = 0; i < slots.Length; i++)
    {
        if (slots[i].CurrentValue != 0)
            return false;
    }

    return true;
}
}
