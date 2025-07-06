using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public Image iconImage;
    public Text countText;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();   
        item.droppedSuccessfully = true;

        ItemData data = DragManager.Instance.draggingItemData;

        if (data == null) return;

        iconImage.sprite = data.icon;
        iconImage.enabled = true;

        int amount = PlayerInventory.Instance.itemDict[data];
        countText.text = amount.ToString();

        Debug.Log("슬롯에 아이템 정보만 적용됨 (오브젝트는 이동 안 함)");
    }
}
