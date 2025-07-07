using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotItemHandler : MonoBehaviour
{
    QuickSlot quickSlot;

    [Header("선택")]
    public bool isSelected = false;

    void Start()
    {
        quickSlot = GetComponent<QuickSlot>();
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

        switch (quickSlot.itemData.itemType)
        {
            case ItemType.Tower:
                UseTowerItem();
                break;
            case ItemType.Food:
                break;
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
