
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class QuickSlotManager : SingleTon<QuickSlotManager>
{

    [Header("메인 퀵슬롯")]
    public List<GameObject> quickSlotPrefabs;
    public GameObject hightLight;

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


    /// <summary>
    /// 퀵슬롯 선택 기능을 담당하는 메서드
    /// </summary>
    /// <param name="index"></param>
    void SelectedQuickSlot(int index)
    {
        UseItemHandler useItemHandler = quickSlotPrefabs[index].GetComponent<UseItemHandler>();
        useItemHandler.isSelected = true;

        RectTransform rectTransform = hightLight.GetComponent<RectTransform>();
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x = 110f * index;
        rectTransform.anchoredPosition = pos;

        for (int i = 0; i < 9; i++)
        {
            if (i != index)
            {
                UseItemHandler otherUseItemHandler = quickSlotPrefabs[i].GetComponent<UseItemHandler>();
                otherUseItemHandler.isSelected = false;
            }
        }
    }
}
