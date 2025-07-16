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

    private Dictionary<ResourceData, ResourceInventorySlot> resourceSlotDict = new Dictionary<ResourceData, ResourceInventorySlot>();


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


    public void UpdateResourceInventoryUI(ResourceData data, int amount)
    {
        if (resourceSlotDict.TryGetValue(data, out var slot)) // ResourceData로 접근
        {
            slot.SetData(data, amount); // 기존 슬롯 수량 갱신
        }
        else
        {
            GameObject slotPrefab = Instantiate(resourceInventorySlotPrefab, resourceInventoryPanel);
            ResourceInventorySlot newSlot = slotPrefab.GetComponent<ResourceInventorySlot>();
            newSlot.SetData(data, amount); // 올바르게 ResourceData 넘김
            resourceSlotDict.Add(data, newSlot); // 새로운 슬롯 등록
        }
    }

}
