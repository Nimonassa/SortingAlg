using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MergeSortTutorialUI : MonoBehaviour
{
    [Header("Assign scene GameObjects from Hierarchy")]
    [SerializeField] private List<NumberBlock> blocks = new List<NumberBlock>();

    [Header("UI")]
    public TextMeshProUGUI tutorialText;

    [Header("Movement")]
    [Tooltip("UI units to move left/right. Increase if 2 is too small for your canvas.")]
    public float moveAmount = 100f;   // default visible amount
    [Tooltip("Units per second for MoveTo")]
    public float moveSpeed = 800f;

    bool tutorialRunning = false;

    void Awake()
    {
        // Log inspector assignments (helps confirm scene instances)
        if (blocks != null && blocks.Count > 0)
        {
            Debug.Log($"MergeSortTutorialUI Awake: {blocks.Count} block(s) assigned in inspector.");
            for (int i = 0; i < blocks.Count; i++)
            {
                var nb = blocks[i];
                if (nb == null)
                {
                    Debug.LogWarning($"blocks[{i}] is null in inspector.");
                    continue;
                }
                bool isSceneObject = nb.gameObject.scene.IsValid();
                Debug.Log($"blocks[{i}] = {nb.gameObject.name} sceneInstance={isSceneObject} anchoredPos={nb.GetComponent<RectTransform>().anchoredPosition}");
                if (!isSceneObject)
                    Debug.LogWarning($"blocks[{i}] appears to be a prefab asset, not a scene instance. Drag from Hierarchy.");
            }
        }
        else
        {
            // Only auto-find if inspector list is empty
            NumberBlock[] found = GetComponentsInChildren<NumberBlock>(true);
            if (found != null && found.Length > 0)
            {
                blocks = new List<NumberBlock>(found);
                Debug.Log($"MergeSortTutorialUI Awake: Auto-found {blocks.Count} NumberBlock(s) as children.");
            }
            else
            {
                Debug.LogWarning("MergeSortTutorialUI Awake: No blocks assigned and none found as children.");
            }
        }
    }

    void Update()
    {
        // Optional: start with Space (works with new Input System if present)
        bool startPressed = false;
        if (Keyboard.current != null)
            startPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        else
        {
            try { startPressed = UnityEngine.Input.GetKeyDown(KeyCode.Space); }
            catch { }
        }

        if (startPressed && !tutorialRunning)
            StartCoroutine(StartMergeSort());
    }

    // Call this from a UI Button to trigger the split
    public void StartMergeSortPublic()
    {
        if (!tutorialRunning) StartCoroutine(StartMergeSort());
    }

    IEnumerator StartMergeSort()
    {
        tutorialRunning = true;
        if (tutorialText != null) tutorialText.text = "Merge Sort: splitting into two halves.";
        yield return new WaitForSeconds(0.15f);
        yield return StartCoroutine(SplitFirstStage());
        tutorialRunning = false;
    }

    IEnumerator SplitFirstStage()
    {
        int count = blocks != null ? blocks.Count : 0;
        if (count == 0)
        {
            Debug.LogWarning("MergeSortTutorialUI: No blocks assigned or found.");
            yield break;
        }

        Debug.Log("SplitFirstStage: starting movement. moveAmount=" + moveAmount);

        // Move first 3 blocks left
        for (int i = 0; i < 3 && i < count; i++)
        {
            var nb = blocks[i];
            if (nb == null) continue;
            RectTransform rt = nb.GetComponent<RectTransform>();
            if (rt == null) continue;
            Vector2 start = rt.anchoredPosition;
            Vector2 target = new Vector2(start.x - moveAmount, start.y);
            Debug.Log($"Block {i} ({nb.gameObject.name}) target left: {target}");
            StartCoroutine(nb.MoveTo(target, moveSpeed));
        }

        // Move next 3 blocks right
        for (int i = 3; i < 6 && i < count; i++)
        {
            var nb = blocks[i];
            if (nb == null) continue;
            RectTransform rt = nb.GetComponent<RectTransform>();
            if (rt == null) continue;
            Vector2 start = rt.anchoredPosition;
            Vector2 target = new Vector2(start.x + moveAmount, start.y);
            Debug.Log($"Block {i} ({nb.gameObject.name}) target right: {target}");
            StartCoroutine(nb.MoveTo(target, moveSpeed));
        }

        // Wait until those blocks finish moving
        yield return StartCoroutine(WaitUntilAllBlocksIdle(0, Mathf.Min(6, count)));

        Debug.Log("SplitFirstStage: movement complete. Final positions:");
        for (int i = 0; i < Mathf.Min(6, count); i++)
        {
            var rt = blocks[i].GetComponent<RectTransform>();
            Debug.Log($"Block {i} final anchoredPos: {rt.anchoredPosition}");
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator WaitUntilAllBlocksIdle(int startIndex, int endIndex)
    {
        endIndex = Mathf.Min(endIndex, blocks.Count);
        bool anyMoving;
        do
        {
            anyMoving = false;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (blocks[i] != null && blocks[i].IsMoving)
                {
                    anyMoving = true;
                    break;
                }
            }
            yield return null;
        } while (anyMoving);
    }
}
