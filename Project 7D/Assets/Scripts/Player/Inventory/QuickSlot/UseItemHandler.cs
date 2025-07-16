using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemHandler : MonoBehaviour
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
            StartCoroutine(UseItem());
        }
    }

    IEnumerator UseItem()
    {
        if (!isSelected) yield break;

        switch (quickSlot.itemData.itemType)
        {
            case ItemType.Tower:
                yield return StartCoroutine(UseTowerItem());
                break;
            case ItemType.Food:
                UseFoodItem();
                break;
        }
    }


    public IEnumerator UseTowerItem()
    {
        // 설치할 프리팹 설정
        TowerItemData towerData = quickSlot.itemData as TowerItemData;
        
        if (towerData != null)
        {
            BuildManager.Instance.BuildingPrefab = towerData.prefab;
            BuildManager.Instance.PreviewPrefab = towerData.prefab;
        }

        // 빌드 모드 켜기
        BuildManager.Instance.ToggleBuildMode();

        yield return new WaitUntil(() => !BuildManager.Instance.isBuildMode);

        if (BuildManager.Instance.isPlaced)
            ConsumeQuickSlotItem();
    }

    void UseFoodItem()
    {
        ConsumeQuickSlotItem();
    }

    void ConsumeQuickSlotItem()
    {
        PlayerInventory.Instance.itemDict[quickSlot.itemData]--;
        quickSlot.SetData(quickSlot.itemData.icon, quickSlot.itemData, PlayerInventory.Instance.itemDict[quickSlot.itemData]);
        quickSlot.LinkData(quickSlot.itemData.icon, quickSlot.itemData, PlayerInventory.Instance.itemDict[quickSlot.itemData]);

        if (PlayerInventory.Instance.itemDict[quickSlot.itemData] == 0)
        {
            quickSlot.ReSetData();
            quickSlot.ResetLinkData();
        }
    }
}
