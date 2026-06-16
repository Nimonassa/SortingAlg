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

    [Header("Spawner")]
    public MergeSortSpawner mergeSortSpawner;

    private int currentMessageIndex = 0;
    private bool isTyping = false;
    public bool tutorialFinished = false;

    private void Start()
    {
        if (messages.Length > 0)
        {
            StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
    }

    public void NextMessage()
    {
        if (isTyping || tutorialFinished)
            return;

        currentMessageIndex++;

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

        Debug.Log("Explanation finished. Spawning merge sort...");

        if (mergeSortSpawner != null)
        {
            mergeSortSpawner.SpawnBlocks();
        }
        else
        {
            Debug.LogWarning("No MergeSortSpawner connected!");
        }
    }
}