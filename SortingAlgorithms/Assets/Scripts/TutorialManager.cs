using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI tutorialText;

    [Header("Number Block UI")]
    public GameObject numberBlockPanel;

    public MergeSortTutorial mergeSortTutorial;


    [Header("Messages")]
    [TextArea]
    public string[] messages;


    [Header("Typing")]
    public float typingSpeed = 0.05f;


    private int currentMessageIndex = 0;
    private bool isTyping = false;

    public bool tutorialFinished = false;



    private void Start()
    {
        // Hide blocks at the start
        if (numberBlockPanel != null)
        {
            numberBlockPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No Number Block Panel assigned!");
        }


        // Start first tutorial message
        if (messages.Length > 0)
        {
            StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
    }




    public void NextMessage()
    {
        // Don't continue while typing
        if (isTyping || tutorialFinished)
            return;


        currentMessageIndex++;


        // Last message finished
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
        tutorialFinished = true;


        tutorialText.text = "";


        Debug.Log("Tutorial finished. Showing number blocks.");

        if (numberBlockPanel != null)
        {

            numberBlockPanel.SetActive(true);

            mergeSortTutorial.Init();
        }
    }
}