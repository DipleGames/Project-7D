using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerInventory : SingleTon<PlayerInventory>
{
    public Dictionary<ResourceType, int> resourceDict = new();
    public Dictionary<ItemData, int> itemDict = new();
    public event Action<Sprite, ResourceType, Category, int> OnResourceChanged;
    public event Action<Sprite, ItemData> OnItemChanged;

    void Start()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resourceDict[type] = 0;
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
    public void AddItem(Sprite icon, ItemData data, int amount)
    {
        if (itemDict.ContainsKey(data))
        {
            itemDict[data] += amount;
        }
        else
        {
            itemDict[data] = amount;
        }
        OnItemChanged?.Invoke(icon, data);

        foreach (var id in itemDict)
            Debug.Log($"{id.Key}, {id.Value}");
    }

    public void SubtractItem(Sprite icon, ItemData data, int amount)
    {
        if (itemDict.ContainsKey(data))
        {
            itemDict[data] -= amount;
        }
        else
        {
            itemDict[data] = amount;
        }
        OnItemChanged.Invoke(icon, data);
    }


    public int GetAmount(ResourceType type)
    {
        return resourceDict.TryGetValue(type, out int val) ? val : 0;
    }
}
