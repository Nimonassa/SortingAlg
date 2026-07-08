using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartAlgorithm1()
    {
        SceneManager.LoadScene("Alg1");
    }

public void MergeSortFinished()
{
    Debug.Log("Merge Sort tutorial finished.");

    SceneManager.LoadScene("Alg1");
}
}