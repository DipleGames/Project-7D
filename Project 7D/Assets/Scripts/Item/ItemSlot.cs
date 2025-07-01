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
        ShopManager.Instance.buyBtn.onClick.AddListener(BuyItem);

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
            resourceNames.Add(req.resourceType.ToString());
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

    public void BuyItem()
    {
        foreach (ResourceRequirement req in currentData.requirements)
        {
            if (PlayerInventory.Instance.resourceDict[req.resourceType] < req.amount)
            {
                Debug.Log("재료가 부족합니다.");
                return;
            }
            PlayerInventory.Instance.SubtractResource(req.resourceIcon, req.resourceType, req.category, req.amount);
        }
        PlayerInventory.Instance.AddItem(currentData.icon, currentData.itemType, currentData.category, 1);
    }

    public void OnClickItem()
    {
        ShopManager.Instance.panel.SetActive(true);
        ShopManager.Instance.buyPanel.SetActive(true);

    }

    public void CancelBtn()
    {
        ShopManager.Instance.panel.SetActive(false);
        ShopManager.Instance.buyPanel.SetActive(false);
    }
}
