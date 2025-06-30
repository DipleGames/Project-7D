using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData currentData;

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // 더블클릭 판정 시간 (초)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                OnClickItem();

                lastClickTime = 0f; // 초기화
            }
            else
            {
                lastClickTime = Time.time;
            }
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

        foreach (var req in currentData.requirements)
        {
            string requirements_Resource_text = string.Join(", ", req.resourceType);
            ShopManager.Instance.requirements_Resource.text = $"필요한 재료 : {requirements_Resource_text}";
            ShopManager.Instance.requirements_Value.text = $"필요한 개수 : {req.amount}";
        }

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
    }

    public void OnClickItem()
    {
        ShopManager.Instance.panel.SetActive(true);
        ShopManager.Instance.buyPanel.SetActive(true);

        ShopManager.Instance.buyBtn.onClick.AddListener(BuyItem);
    }

    public void CancleBtn()
    {
        ShopManager.Instance.panel.SetActive(false);
        ShopManager.Instance.buyPanel.SetActive(false);
    }
}
