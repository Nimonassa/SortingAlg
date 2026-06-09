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

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;
    public float delayBetweenMessages = 1.5f;

    private void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    private IEnumerator TutorialSequence()
    {
        foreach (string message in messages)
        {
            yield return StartCoroutine(TypeText(message));
            yield return new WaitForSeconds(delayBetweenMessages);
        }

        // Tutorial finished
        tutorialText.text = "";

        // Start sorting demonstration here
        // StartCoroutine(BubbleSortDemo());
    }

    private IEnumerator TypeText(string message)
    {
        tutorialText.text = "";

        foreach (char letter in message)
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}