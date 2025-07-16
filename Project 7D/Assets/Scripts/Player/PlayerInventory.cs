using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerInventory : SingleTon<PlayerInventory>
{
    public Dictionary<ResourceData, int> resourceDict = new();
    public Dictionary<ItemData, int> itemDict = new();

    public event Action<ResourceData, int> OnResourceChanged;
    public event Action<Sprite, ItemData> OnItemChanged;

    void Start()
    {
        // 초기화는 필요에 따라 외부에서 ResourceData들을 수집해 설정해야 함
        // 예: Resources.LoadAll<ResourceData>("Resources") 등
    }

    /// <summary>
    /// 자원 추가 및 제거
    /// </summary>
    /// <param name="data"></param>
    /// <param name="amount"></param>
    public void AddResource(ResourceData data, int amount)
    {
        if (resourceDict.ContainsKey(data))
        {
            resourceDict[data] += amount;
        }
        else
        {
            resourceDict[data] = amount;
        }

        OnResourceChanged?.Invoke(data, resourceDict[data]);
    }

    public void SubtractResource(ResourceData data, int amount)
    {
        if (resourceDict.ContainsKey(data))
        {
            resourceDict[data] -= amount;
        }
        else
        {
            resourceDict[data] = amount;
        }

        OnResourceChanged?.Invoke(data, resourceDict[data]);
    }

    public int GetAmount(ResourceData data)
    {
        return resourceDict.TryGetValue(data, out int val) ? val : 0;
    }

    /// <summary>
    /// 아이템 추가 및 제거
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="data"></param>
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
}
