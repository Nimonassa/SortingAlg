using System.Collections.Generic;
using UnityEngine;

public class OutputSlotsController : MonoBehaviour
{
    [Header("Output Slot Panels")]
    public List<GameObject> outputSlots = new List<GameObject>();

    void Start()
    {
        HideSlots();
    }

    public void ShowSlots()
    {
        foreach (GameObject slot in outputSlots)
        {
            if (slot != null)
                slot.SetActive(true);
        }
    }

    public void HideSlots()
    {
        foreach (GameObject slot in outputSlots)
        {
            if (slot != null)
                slot.SetActive(false);
        }
    }
}