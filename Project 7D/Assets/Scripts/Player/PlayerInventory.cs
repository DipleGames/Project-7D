using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerInventory : SingleTon<PlayerInventory>
{
    public Dictionary<ResourceType, int> resourceDict = new();
    public Dictionary<ItemType, int> itemDict = new();
    public event Action<Sprite, ResourceType, Category, int> OnResourceChanged;
    public event Action<Sprite, ItemType, int> OnItemChanged;

    void Start()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resourceDict[type] = 0;
        }

        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            itemDict[type] = 0;
        }
    }

    /// <summary>
    /// 자원 추가 및 제거
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="type"></param>
    /// <param name="category"></param>
    /// <param name="amount"></param>
    public void AddResource(Sprite icon, ResourceType type, Category category, int amount)
    {
        if (resourceDict.ContainsKey(type))
        {
            resourceDict[type] += amount;
        }
        else
        {
            resourceDict[type] = amount;
        }

        // UI에 현재 수량 전달
        OnResourceChanged?.Invoke(icon, type, category, resourceDict[type]);
    }

    public void SubtractResource(Sprite icon, ResourceType type, Category category, int amount)
    {
        if (resourceDict.ContainsKey(type))
        {
            resourceDict[type] -= amount;
        }
        else
        {
            resourceDict[type] = amount;
        }

        // UI에 현재 수량 전달
        OnResourceChanged?.Invoke(icon, type, category, resourceDict[type]);
    }

    /// <summary>
    /// 아이템 추가 및 제거
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="type"></param>
    /// <param name="category"></param>
    /// <param name="amount"></param>
    public void AddItem(Sprite icon, ItemType type, Category category, int amount)
    {
        if (itemDict.ContainsKey(type))
        {
            itemDict[type] += amount;
        }
        else
        {
            itemDict[type] = amount;
        }
        OnItemChanged?.Invoke(icon, type, itemDict[type]);
    }

    public void SubtractItem(Sprite icon, ItemType type, Category category, int amount)
    {
        if (itemDict.ContainsKey(type))
        {
            itemDict[type] -= amount;
        }
        else
        {
            itemDict[type] = amount;
        }
        OnItemChanged.Invoke(icon, type, itemDict[type]);
    }


    public int GetAmount(ResourceType type)
    {
        return resourceDict.TryGetValue(type, out int val) ? val : 0;
    }
}
