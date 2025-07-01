using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : SingleTon<ShopManager>
{
    [SerializeField] private bool inShopArea;

    [Header("아이템 샵")]
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject itemSlotPanel;
    public GameObject panel;
    public GameObject buyPanel;
    public Button buyBtn;

    public Text itemName;
    public Text itemDesc;
    public Text requirements_Resource;
    public Text requirements_Value;
    [SerializeField] private List<ItemData> ToweritemDatas;
    [SerializeField] private List<ItemData> FooditemDatas;

    private List<GameObject> itemSlotList;

    void Update()
    {
        if (inShopArea)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!shopUI.activeSelf)
                    shopUI.SetActive(true);
                else
                    shopUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (shopUI.activeSelf)
                {
                    if (buyPanel.activeSelf)
                    {
                        buyPanel.SetActive(false);
                        panel.SetActive(false);
                    }
                    else
                    {
                        shopUI.SetActive(false);
                    }
                }
            }
        }
        else if (!inShopArea)
        {
            if (shopUI.activeSelf) shopUI.SetActive(false);
        }
    }

    void Start()
    {
        itemSlotList = new List<GameObject>();

        foreach (ItemData itemData in ToweritemDatas)
        {
            GameObject itemSlotInstance = Instantiate(itemSlotPrefab, itemSlotPanel.transform); ;
            SetItemData(itemSlotInstance, itemData);
            itemSlotInstance.SetActive(false);
            itemSlotList.Add(itemSlotInstance);
        }

        foreach (ItemData itemData in FooditemDatas)
        {
            GameObject itemSlotInstance = Instantiate(itemSlotPrefab, itemSlotPanel.transform); ;
            SetItemData(itemSlotInstance, itemData);
            itemSlotInstance.SetActive(false);
            itemSlotList.Add(itemSlotInstance);
        }
        
        foreach (GameObject itemSlot in itemSlotList)
        {
            ItemSlot itemSlotComponent = itemSlot.GetComponent<ItemSlot>();

            if (itemSlotComponent.currentData.itemType == ItemType.Tower)
            {
                itemSlot.SetActive(true);
            }
            else
            {
                itemSlot.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inShopArea = true;
    }

    void OnTriggerExit(Collider other)
    {
        inShopArea = false;
    }

    void SetItemData(GameObject itemSlotInstance, ItemData itemData)
    {
        ItemSlot itemSlot = itemSlotInstance.GetComponent<ItemSlot>();
        itemSlot.currentData = itemData;
    }

    public void OnItemBtnClicked()
    {
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;

        Text itemTypeText = clickedObj.GetComponentInChildren<Text>(); // (구버전 Text)

        if (itemTypeText.text == "타워")
        {
            foreach (GameObject itemSlot in itemSlotList)
            {
                ItemSlot itemSlotComponent = itemSlot.GetComponent<ItemSlot>();

                if (itemSlotComponent.currentData.itemType == ItemType.Tower)
                {
                    itemSlot.SetActive(true);
                }
                else
                {
                    itemSlot.SetActive(false);
                }
            }
        }
        else if (itemTypeText.text == "음식")
        {
            foreach (GameObject itemSlot in itemSlotList)
            {
                ItemSlot itemSlotComponent = itemSlot.GetComponent<ItemSlot>();

                if (itemSlotComponent.currentData.itemType == ItemType.Food)
                {
                    itemSlot.SetActive(true);
                }
                else
                {
                    itemSlot.SetActive(false);
                }
            }
        }

    }
}
