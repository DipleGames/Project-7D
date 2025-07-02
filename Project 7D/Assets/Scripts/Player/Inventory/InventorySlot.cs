using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text count;
    public ItemData itemData;

    public bool isEmpty = true;


    public void SetData(Sprite iconSprite, ItemData data, int amount)
    {
        icon.sprite = iconSprite;
        count.text = amount.ToString();
        itemData = data;
        isEmpty = false;
    }

    public void ReSetData()
    {
        icon.sprite = null;
        count.text = "";
        itemData = null;
        isEmpty = true;
    }
}
