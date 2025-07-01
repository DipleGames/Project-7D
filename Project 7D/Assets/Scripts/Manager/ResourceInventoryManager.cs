using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceInventoryManager : SingleTon<ResourceInventoryManager>
{
    [Header("리소스 인벤토리 UI")]
    [SerializeField] private GameObject resourceInventoryUI;
    [SerializeField] private Transform resourceInventoryPanel;
    [SerializeField] private GameObject resourceInventorySlotPrefab;

    private Dictionary<ResourceType, ResourceInventorySlot> resourceSlotDict = new Dictionary<ResourceType, ResourceInventorySlot>();


    void Start()
    {
        PlayerInventory.Instance.OnResourceChanged += UpdateResourceInventoryUI;
    }

    private bool isOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            resourceInventoryUI.SetActive(isOpen);
        }
    }


    public void UpdateResourceInventoryUI(Sprite icon, ResourceType type, Category category, int amount)
    {
        if (resourceSlotDict.TryGetValue(type, out var slot)) // 만약 인벤에 이미 있는 아이템이라면
        {
            slot.SetData(icon, type.ToString(), category.ToString(), amount); // 수량만 갱신
        }
        else
        {
            GameObject slotPrefab = Instantiate(resourceInventorySlotPrefab, resourceInventoryPanel);
            ResourceInventorySlot newSlotPrefab = slotPrefab.GetComponent<ResourceInventorySlot>();
            newSlotPrefab.SetData(icon, type.ToString(), category.ToString(), amount);
            resourceSlotDict.Add(type, newSlotPrefab); // 새로 등록
        }
    }

}
