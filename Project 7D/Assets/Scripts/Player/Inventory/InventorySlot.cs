using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text count;
    public ItemType itemType;

    public bool isEmpty = true;


    public void SetData(Sprite iconSprite, ItemType type, int amount)
    {
        icon.sprite = iconSprite;
        count.text = amount.ToString();
        itemType = type;
        isEmpty = false;
    }
}
