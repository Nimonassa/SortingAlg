
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
            "In row 3, demonstrate the merge between the two sorted halves.\n\n" +
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


    // =========================
    // CHECK CORRECT ANSWERS
    // =========================

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
            if (divideSlots[i].gameObject.GetComponent<NumberBlock>().value != expected[i])
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
            if (sortSlots[i].gameObject.GetComponent<NumberBlock>().value != expected[i])
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
            if (mergeSlots[i].gameObject.GetComponent<NumberBlock>().value != expected[i])
                return false;
        }

        return true;
    }


    // =========================
    // SUBMIT BUTTON
    // =========================
public void SubmitAnswer()
{
    if (!AreAllSlotsFilled())
    {
        feedbackText.text = "Please make sure all slots are filled.";
        return;
    }

        if (!CheckDivide())
    {
        feedbackText.text = "Please make sure divide row is filled in correctly and click Submit again.";
        return;
    }

        if (!CheckSort())
    {
        feedbackText.text = "Please make sure Sort row is filled in correctly and click Submit again.";
        return;
    }

            if (!CheckMerge())
    {
        feedbackText.text = "Please make sure Merge row is filled in correctly and click Submit again.";
        return;
    }

    feedbackText.text = "All slots are filled and correct! Good job!";


}

bool AreAllSlotsFilled()
{
    foreach (DropSlot slot in divideSlots)
    {
        NumberBlock nslot = slot.gameObject.GetComponent<NumberBlock>();
        if (slot == null || nslot.value == 0)
            return false;
    }

    foreach (DropSlot slot in sortSlots)
    {
        NumberBlock nslot = slot.gameObject.GetComponent<NumberBlock>();
        if (slot == null || nslot.value == 0)
            return false;
    }

    foreach (DropSlot slot in mergeSlots)
    {
        NumberBlock nslot = slot.gameObject.GetComponent<NumberBlock>();
        if (slot == null || nslot.value == 0)
            return false;
    }

    return true;
}
    // =========================
    // CHECK IF EVERY SLOT
    // IN A ROW IS FILLED
    // =========================

    bool IsRowFilled(DropSlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
                return false;

            if (slots[i].CurrentValue == -1)
                return false;
        }
        return true;
    }
}
