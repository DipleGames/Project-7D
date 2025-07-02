using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType
{
    None,
    Tower,
    Food
}

public enum Category
{
    Structure,
    Consumable,
    resource,
}


[System.Serializable]
public class ResourceRequirement // 필요한 재료 및 개수 정의하기 위함
{
    public ResourceType resourceType;
    public Sprite resourceIcon;
    public Category category;
    public int amount;
}



[CreateAssetMenu(fileName = "New ItemData", menuName = "Game/Item Data")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public string desc;
    public Sprite icon;
    public ItemType itemType;
    public Category category;
    public List<ResourceRequirement> requirements;
}
