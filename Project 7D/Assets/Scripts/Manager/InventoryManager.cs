using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    [Header("인벤토리 UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject inventorySlotPanel;

    [SerializeField] private GameObject[] inventorySlots;

    void Start()
    {
        PlayerInventory.Instance.OnItemChanged += UpdateInventoryUI;
    }

    private bool isOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);
        }
    }

    void UpdateInventoryUI(ItemData data)
    {
        Dictionary<ItemData, int> dict = PlayerInventory.Instance.itemDict;

        // 1. 먼저 같은 아이템이 있는지 검사해서 스택
        foreach (GameObject slot in inventorySlots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (!inventorySlot.isEmpty && inventorySlot.itemData == data)
            {
                inventorySlot.SetData(data, dict[data]);
                return;
            }
        }

        // 2. 없으면 빈 칸에 새로 배치
        foreach (GameObject slot in inventorySlots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot.isEmpty)
            {
                if (dict[data] <= 0) return;
                
                inventorySlot.SetData(data, dict[data]);
                return;
            }
        }
    }
    
    /// <summary>
    /// 필터기능(추가예정)
    /// </summary>
    /// <param name="filterType"></param>
    public void FilterByItemType(ItemType filterType)
    {
        Dictionary<ItemData, int> dict = PlayerInventory.Instance.itemDict;

        foreach (GameObject slot in inventorySlots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.ReSetData();
        }


        int i = 0;
        foreach (var d in dict)
        {
            ItemData itemData = d.Key;
            if (itemData.itemType == filterType)
            {
                InventorySlot inventorySlot = inventorySlots[i].GetComponent<InventorySlot>();
                inventorySlot.SetData(itemData, dict[itemData]);
                i++;
            }
        }
    }

    public void OnClickedTowerTypeBtn()
    {
        FilterByItemType(ItemType.Tower);
    }

    public void OnClickedFoodTypeBtn()
    {
        FilterByItemType(ItemType.Food);
    }
}
