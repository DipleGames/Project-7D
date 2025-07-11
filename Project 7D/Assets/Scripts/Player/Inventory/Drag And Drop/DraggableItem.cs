using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform inventoryslotPrefab;
    private CanvasGroup canvasGroup;

    [Header("디버그")]
    public InventorySlot inventorySlot;
    public bool droppedSuccessfully = false;



    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryslotPrefab = transform.parent;
        inventorySlot = inventoryslotPrefab.GetComponent<InventorySlot>();

        Vector2 originalSize = GetComponent<RectTransform>().sizeDelta;
        DragManager.Instance.BeginDrag(inventorySlot.itemData, originalSize);

        transform.SetParent(transform.root); // UI 최상단으로 이동 (가리거나 잘리는 것 방지)
        canvasGroup.blocksRaycasts = false;  // Drop 인식 위해 false
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragManager.Instance.UpdateDrag(eventData.position);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(inventoryslotPrefab);
        transform.localPosition = Vector3.zero;

        DragManager.Instance.EndDrag();
        droppedSuccessfully = false;
        canvasGroup.blocksRaycasts = true;
    }


}