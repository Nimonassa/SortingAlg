using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI tutorialText;

    [Header("Messages")]
    [TextArea]
    public string[] messages;

    [Header("Typing")]
    public float typingSpeed = 0.05f;


    [Header("Sorting Tutorial")]
    public InsertionSortTutorial insertionSortTutorial;


    private int currentMessageIndex = 0;
    private bool isTyping = false;


    private void Start()
    {
        if (messages.Length > 0)
        {
            StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
    }


    public void NextMessage()
    {
        if (isTyping)
            return;


        currentMessageIndex++;


        // Finished explanation section
        if (currentMessageIndex >= messages.Length)
        {
            TutorialFinished();
            return;
        }


        StartCoroutine(TypeText(messages[currentMessageIndex]));
    }



    private IEnumerator TypeText(string message)
    {
        isTyping = true;

        tutorialText.text = "";


        foreach (char letter in message)
        {
            tutorialText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }


        isTyping = false;
    }



    private void TutorialFinished()
    {
        tutorialText.text = "";

        Debug.Log("Explanation finished. Starting insertion sort...");


        // starting insertion sort tutorial
        if (insertionSortTutorial != null)
        {
            insertionSortTutorial.StartSorting();

        }
        else
        {
            Debug.LogWarning("No InsertionSortTutorial connected!");
        }
    }
}