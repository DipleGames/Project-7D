using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType
{
    Tower,
    Food
}

[System.Serializable]
public class ResourceRequirement
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
    public List<ResourceRequirement> requirements;
}
