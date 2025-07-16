using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemData currentData;

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // 더블클릭 판정 시간 (초)

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            OnClickItem();
            lastClickTime = 0f;
        }
        else
        {
            lastClickTime = Time.time;
        }
    }

    void Start()
    {
        Image icon = GetComponent<Image>();
        icon.sprite = currentData.icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopManager.Instance.itemName.text = currentData.displayName;
        ShopManager.Instance.itemDesc.text = currentData.desc;

        List<string> resourceNames = new();
        List<string> amounts = new();

        foreach (var req in currentData.requirements)
        {
            resourceNames.Add(req.resourceData.displayName);
            amounts.Add(req.amount.ToString());
        }

        ShopManager.Instance.requirements_Resource.text = $"필요한 재료 : {string.Join(", ", resourceNames)}";
        ShopManager.Instance.requirements_Value.text = $"필요한 개수 : {string.Join(", ", amounts)}";


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopManager.Instance.itemName.text = "아이템 이름";
        ShopManager.Instance.itemDesc.text = "아이템 설명입니다.";
        ShopManager.Instance.requirements_Resource.text = $"필요한 재료 : ";
        ShopManager.Instance.requirements_Value.text = $"필요한 개수 : ";
    }

    public void BuyItem() // 아이템을 구매하는 메서드
    {
        foreach (ResourceRequirement req in currentData.requirements)
        {
            if (PlayerInventory.Instance.resourceDict[req.resourceData] < req.amount)
            {
                Debug.Log("재료가 부족합니다.");
                return;
            }
            PlayerInventory.Instance.SubtractResource(req.resourceData, req.amount);
        }
        PlayerInventory.Instance.AddItem(currentData.icon, currentData, 1); // 플레이어 인벤토리에 아이템을 추가함 (아이템의 아이콘과 아이템의 데이터 , 개수)
    }

    public void OnClickItem()
    {
        ShopManager.Instance.selectedItemSlot = this; // 현재 슬롯 등록
        ShopManager.Instance.panel.SetActive(true);
        ShopManager.Instance.buyPanel.SetActive(true);
    }

    public void CancelBtn()
    {
        ShopManager.Instance.panel.SetActive(false);
        ShopManager.Instance.buyPanel.SetActive(false);
    }
}
