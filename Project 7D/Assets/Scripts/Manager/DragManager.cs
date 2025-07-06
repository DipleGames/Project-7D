using UnityEngine;

public class DragManager : SingleTon<DragManager>
{
    public ItemData draggingItemData;

    public void BeginDrag(ItemData data)
    {
        draggingItemData = data;
    }

    public void EndDrag()
    {
        draggingItemData = null;
    }
}