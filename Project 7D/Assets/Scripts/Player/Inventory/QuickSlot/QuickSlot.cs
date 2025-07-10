using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : InventorySlot
{
    [Header("메인 퀵슬롯")]
    public GameObject linkedQuickSlot;

    public void LinkData(Sprite icon, ItemData data, int amount)
    {
        QuickSlot quickSlot = linkedQuickSlot.GetComponent<QuickSlot>();
        quickSlot.SetData(icon, data, amount);
    }

    public void ResetLinkData()
    {
        QuickSlot quickSlot = linkedQuickSlot.GetComponent<QuickSlot>();
        quickSlot.ReSetData();
    }
}
