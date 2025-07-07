
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : SingleTon<QuickSlotManager>
{
    [Header("인벤 퀵슬롯")]
    public List<GameObject> quickSlotPrefabs_Inven;

    [Header("메인 퀵슬롯")]
    public List<GameObject> quickSlotPrefabs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectedQuickSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectedQuickSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectedQuickSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectedQuickSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectedQuickSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectedQuickSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            SelectedQuickSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
           SelectedQuickSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            SelectedQuickSlot(8);
    }

    public void SyncQuickSlotsFromInven()
    {
        for (int i = 0; i < quickSlotPrefabs_Inven.Count; i++)
        {
            QuickSlot quickSlot = quickSlotPrefabs_Inven[i].GetComponent<QuickSlot>();
            QuickSlot mainSlot = quickSlotPrefabs[i].GetComponent<QuickSlot>();

            if (quickSlot.itemData != null)
            {
                mainSlot.SetData(quickSlot.itemData.icon, quickSlot.itemData, PlayerInventory.Instance.itemDict[quickSlot.itemData]); // 아이템 데이터와 아이콘을 복사
            }
            else
            {
                mainSlot.ReSetData(); // 비었으면 메인 퀵슬롯도 비우기
            }
        }
    }


    /// <summary>
    /// 퀵슬롯 선택 기능을 담당하는 메서드
    /// </summary>
    /// <param name="index"></param>
    void SelectedQuickSlot(int index)
    {
        QuickSlotItemHandler quickSlotItemHandler = quickSlotPrefabs[index].GetComponent<QuickSlotItemHandler>();
        quickSlotItemHandler.isSelected = true;

        for (int i = 0; i < 9; i++)
        {
            if (i != index)
            {
                QuickSlotItemHandler otherQuickSlotItemHandler = quickSlotPrefabs[i].GetComponent<QuickSlotItemHandler>();
                otherQuickSlotItemHandler.isSelected = false;
            }
        }
    }
}
