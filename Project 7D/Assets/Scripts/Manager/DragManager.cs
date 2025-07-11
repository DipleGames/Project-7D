using UnityEngine;
using UnityEngine.UI;

public class DragManager : SingleTon<DragManager>
{
    public ItemData draggingItemData;

    [Header("UI 설정")]
    [SerializeField] private Canvas canvas; // 반드시 UI Canvas 연결
    [SerializeField] private GameObject dragIconPrefab; // 투명도 0.5짜리 아이콘 프리팹

    private GameObject currentDragIcon;
    private Image dragImage;

    public void BeginDrag(ItemData data, Vector2 originalSize)
    {
        draggingItemData = data;

        if (data == null || dragIconPrefab == null || canvas == null)
            return;

        // 아이콘 생성
        currentDragIcon = Instantiate(dragIconPrefab, canvas.transform);
        dragImage = currentDragIcon.GetComponent<Image>();
        dragImage.sprite = data.icon;
        dragImage.raycastTarget = false;

        // 크기 동기화
        RectTransform iconRect = currentDragIcon.GetComponent<RectTransform>();
        iconRect.sizeDelta = originalSize;

        // 투명도 조절
        var color = dragImage.color;
        color.a = 0.5f;
        dragImage.color = color;
    }

    public void UpdateDrag(Vector2 screenPosition)
    {
        if (currentDragIcon != null)
        {
            currentDragIcon.transform.position = screenPosition;
        }
    }

    public void EndDrag()
    {
        draggingItemData = null;

        if (currentDragIcon != null)
        {
            Destroy(currentDragIcon);
            currentDragIcon = null;
            dragImage = null;
        }
    }
}
