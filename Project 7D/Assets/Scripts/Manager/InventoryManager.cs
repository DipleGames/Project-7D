using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
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

    void UpdateInventoryUI(Sprite icon, ItemType type, int amount)
    {
        // 1. 먼저 같은 아이템이 있는지 검사해서 스택
        foreach (GameObject slot in inventorySlots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (!inventorySlot.isEmpty && inventorySlot.itemType == type)
            {
                inventorySlot.SetData(icon, type, amount);
                return;
            }
        }

        // 2. 없으면 빈 칸에 새로 배치
        foreach (GameObject slot in inventorySlots)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot.isEmpty)
            {
                inventorySlot.SetData(icon, type, amount);
                return;
            }
        }
    }
}
