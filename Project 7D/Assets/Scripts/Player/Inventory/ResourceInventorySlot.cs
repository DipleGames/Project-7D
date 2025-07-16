using UnityEngine;
using UnityEngine.UI;

public class ResourceInventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text type;
    [SerializeField] private Text count;

    public void SetData(ResourceData data, int amount)
    {
        icon.sprite = data.icon;
        itemName.text = data.displayName;
        type.text = data.resourceType.ToString();
        count.text = amount.ToString();
    }
}
