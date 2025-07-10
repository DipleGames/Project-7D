using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotItemHandler : MonoBehaviour
{
    QuickSlot quickSlot;
    Text Count;

    public bool isSelected = false;

    void Start()
    {
        quickSlot = GetComponent<QuickSlot>();
        Count = quickSlot.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UseItem();
        }
    }

    void UseItem()
    {
        if (!isSelected) return;

        PlayerInventory.Instance.itemDict[quickSlot.itemData]--;
        quickSlot.SetData(quickSlot.itemData.icon, quickSlot.itemData, PlayerInventory.Instance.itemDict[quickSlot.itemData]);
        quickSlot.LinkData(quickSlot.itemData.icon, quickSlot.itemData, PlayerInventory.Instance.itemDict[quickSlot.itemData]);

        switch (quickSlot.itemData.itemType)
        {
            case ItemType.Tower:
                UseTowerItem();
                break;
            case ItemType.Food:
                break;
        }

        if (PlayerInventory.Instance.itemDict[quickSlot.itemData] == 0)
        {
            quickSlot.ReSetData();
            quickSlot.ResetLinkData();
        }
    }

    void UseTowerItem()
    {
        // 설치할 프리팹 설정
        BuildManager.Instance.BuildingPrefab = quickSlot.itemData.prefab;
        BuildManager.Instance.PreviewPrefab = quickSlot.itemData.prefab;

        // 빌드 모드 켜기
        BuildManager.Instance.ToggleBuildMode();
    }
}
