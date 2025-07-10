using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();   
        item.droppedSuccessfully = true;

        QuickSlot quickSlot = GetComponent<QuickSlot>();
        ItemData data = DragManager.Instance.draggingItemData;
        quickSlot.itemData = data;

        if (data == null) return;

        quickSlot.SetData(data.icon, data, PlayerInventory.Instance.itemDict[data]);
        quickSlot.LinkData(data.icon, data, PlayerInventory.Instance.itemDict[data]);
        Debug.Log("슬롯에 아이템 정보만 적용됨 (오브젝트는 이동 안 함)");
    }
}
