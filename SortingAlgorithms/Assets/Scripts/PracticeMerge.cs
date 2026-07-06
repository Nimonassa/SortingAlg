using TMPro;
using UnityEngine;

public class PracticeMerge : MonoBehaviour
{
    public enum TutorialStep
    {
        Divide,
        SortHalves,
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
        BeginDivideStep();
    }

    void BeginDivideStep()
    {
        currentStep = TutorialStep.Divide;

        tutorialText.text =
        "Step 1\n\n" +
        "Merge Sort begins by DIVIDING the array.\n\n" +
        "Drag the first three numbers into the LEFT group.\n" +
        "Drag the last three numbers into the RIGHT group.\n\n" +
        "When every number is in the correct half the next step will begin.";
    }

    void BeginSortStep()
    {
        currentStep = TutorialStep.SortHalves;

        tutorialText.text =
        "Great!\n\n" +
        "Now sort BOTH halves individually.\n\n" +
        "Remember that Merge Sort sorts each small section before combining them.\n\n" +
        "Arrange the numbers from smallest to largest inside each half.";
    }

    void BeginMergeStep()
    {
        currentStep = TutorialStep.Merge;

        tutorialText.text =
        "Excellent!\n\n" +
        "Now comes the MERGE.\n\n" +
        "Compare the smallest remaining number from each half.\n\n" +
        "Whichever is smaller goes into the final array.\n\n" +
        "Continue until every number has been merged.";
    }

    void FinishTutorial()
    {
        currentStep = TutorialStep.Finished;

        tutorialText.text =
        "Congratulations!\n\n" +
        "You have completed Merge Sort.\n\n" +
        "The array has been divided, sorted, and merged into one sorted list.";
    }

    public void CheckStep()
    {
        switch(currentStep)
        {
            case TutorialStep.Divide:

                if(CheckDivide())
                    BeginSortStep();

                break;

            case TutorialStep.SortHalves:

                if(CheckSort())
                    BeginMergeStep();

                break;

            case TutorialStep.Merge:

                if(CheckMerge())
                    FinishTutorial();

                break;
        }
    }

    bool CheckDivide()
    {
        int[] expected =
        {
            8,3,1,
            6,2,9
        };

        for(int i=0;i<divideSlots.Length;i++)
        {
            if(divideSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }

    bool CheckSort()
    {
        int[] expected =
        {
            1,3,8,
            2,6,9
        };

        for(int i=0;i<sortSlots.Length;i++)
        {
            if(sortSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }

    bool CheckMerge()
    {
        int[] expected =
        {
            1,2,3,6,8,9
        };

        for(int i=0;i<mergeSlots.Length;i++)
        {
            if(mergeSlots[i].CurrentValue != expected[i])
                return false;
        }

        return true;
    }
}