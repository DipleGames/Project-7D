using UnityEngine;
using UnityEngine.UI;

public class ResourceInventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text category;
    [SerializeField] private Text count;

    public void SetData(Sprite iconSprite, string name, string categoryText, int amount)
    {
        icon.sprite = iconSprite;
        itemName.text = name;
        category.text = categoryText;
        count.text = amount.ToString();
    }
}
