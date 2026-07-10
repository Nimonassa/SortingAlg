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

    private void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(1f);
        BeginDivideStep();

        yield return new WaitForSeconds(10f);
        BeginSortStep();

        yield return new WaitForSeconds(15f);
        BeginMergeStep();

    }

    void BeginDivideStep()
    {
        currentStep = TutorialStep.Divide;

        tutorialText.text =
            "Step 1\n\n" +
            "In the first row, Merge Sort begins by dividing the array into two halves.\n\n" +
            "Drag the first three numbers into the LEFT half.\n" +
            "Drag the last three numbers into the RIGHT half.";
    }

    void BeginSortStep()
    {
        currentStep = TutorialStep.Sort;

        tutorialText.text =
            "Great!\n\n" +
            "In the second row, Now sort BOTH halves separately.\n\n" +
            "Left Half:\n8 12 27\n\n" +
            "Right Half:\n2 11 35";
    }

    void BeginMergeStep()
    {
        currentStep = TutorialStep.Merge;

        tutorialText.text =
            "Excellent!\n\n" +
            "In the third row, demonstrate the merge between the two sorted halves.\n\n" +
            "Compare the first number in each half and drag the smaller one into the final row.\n" +
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
}
