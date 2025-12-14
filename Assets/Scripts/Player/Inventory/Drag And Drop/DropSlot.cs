using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem item = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (item == null) return;

        item.droppedSuccessfully = true;

        InventorySlot originSlot = item.inventorySlot;
        InventorySlot targetSlot = GetComponent<InventorySlot>();
        ItemData draggedData = DragManager.Instance.draggingItemData;

        if (draggedData == null || originSlot == null || targetSlot == null)
            return;

        // 자기 자신에 드롭한 경우 무시
        if (originSlot == targetSlot) return;

        // 도착 슬롯이 비어 있다면 단순 이동
        if (targetSlot.itemData == null)
        {
            int count = PlayerInventory.Instance.itemDict.ContainsKey(draggedData)
                ? PlayerInventory.Instance.itemDict[draggedData]
                : 0;

            targetSlot.SetData(draggedData, count); // 도착 슬롯에 데이터 세팅
            if (targetSlot is QuickSlot tq) // 근데 도착 슬롯이 퀵슬롯이면 링크데이터 세팅까지
            {
                tq.LinkData(draggedData, count);
            }

            if (originSlot is QuickSlot originQS) // 출발슬롯이 퀵슬롯이면
            {
                originSlot.ReSetData(); // 출발슬롯 밀고
                originQS.ResetLinkData(); // 출발슬롯에 연동되어있던 링크슬롯 밀고
            }
            else
            {
                originSlot.ReSetData(); // 출발슬롯이 인벤슬롯이면 데이터만 밀고
            }

            Debug.Log("빈 슬롯으로 이동 완료");
            return;
        }
        else // 도착 슬롯이 비어있지 않다면 스왑
        {
            // 스왑용 데이터 임시 저장 (깊은 복사)
            ItemData tempData = targetSlot.itemData; // 도착슬롯에 있는 데이터를 미리 저장

            int tempCount = PlayerInventory.Instance.itemDict.ContainsKey(tempData) // 도착슬롯에 들어있는 아이템의 갯수
                ? PlayerInventory.Instance.itemDict[tempData]
                : 0;

            int draggedCount = PlayerInventory.Instance.itemDict.ContainsKey(draggedData) // 옮길려고하는 아이템의 갯수
                ? PlayerInventory.Instance.itemDict[draggedData]
                : 0;

            // target ← draggedData
            targetSlot.SetData(draggedData, draggedCount); // 놓았을때 도착슬롯에 데이터 세팅
            if (targetSlot is QuickSlot targetQS) // 근데 도착슬롯이 퀵슬롯이면
                targetQS.LinkData(draggedData, draggedCount); // 링크슬롯까지 세팅

            // origin ← tempData (미리 저장된 아이콘과 수량 사용)
            originSlot.SetData(tempData, tempCount); // 출발슬롯에 저장되어있던 정보 세팅
            if (originSlot is QuickSlot originQS) // 근데 출발슬롯이 퀵슬롯이면
                originQS.LinkData(tempData, tempCount); // 링크슬롯까지 세팅
            
            Debug.Log("슬롯 간 스왑 완료");
        }

    }
}
