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
    Resource,
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
    public string displayName; // 디스플레이 이름
    public string desc; // 아이템 설명
    public Sprite icon; // 아이템 아이콘
    public ItemType itemType; // 아이템의 타입 
    public Category category; // 아이템의 종류(설치, 소비, 자원)
    public List<ResourceRequirement> requirements; // 이 아이템을 구매하기 위한 자원량 리스트
}
