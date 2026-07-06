using UnityEngine;

public class DropSlot : MonoBehaviour
{
    [HideInInspector]
    public NumberBlock currentBlock;

    public int CurrentValue
    {
        get
        {
            if (currentBlock == null)
                return -1;

            return currentBlock.value;
        }
    }
}