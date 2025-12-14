using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : SingleTon<ObjectPool>
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [System.Serializable]
    public class ResourcePoolGroup
    {
        public List<Pool> resourcePools;
    }

    [System.Serializable]
    public class ZombiePoolGroup
    {
        public List<Pool> zombiePools;
    }

    public List<ResourcePoolGroup> resourcePoolGroups;
    private Dictionary<string, Queue<GameObject>> resourcePoolDictionary;
    
    public List<ZombiePoolGroup> zombiePoolGroups;
    private Dictionary<string, Queue<GameObject>> zombiePoolDictionary;

    protected new void Awake()
    {
        ResourcePooling();
        ZombiePooling();
    }


    private Dictionary<string, Transform> poolParents = new(); // 태그별 부모
    void ResourcePooling()
    {
        resourcePoolDictionary = new Dictionary<string, Queue<GameObject>>();
        poolParents = new Dictionary<string, Transform>();

        foreach (ResourcePoolGroup resourcePoolGroup in resourcePoolGroups)
        {

            foreach (Pool resourcePool in resourcePoolGroup.resourcePools)
            {
                // 1. 각 리소스 타입별 부모 생성
                GameObject parentObj = new GameObject($"Resource({resourcePool.tag})");
                parentObj.transform.SetParent(this.transform); // ObjectPool 아래 정리
                poolParents[resourcePool.tag] = parentObj.transform;

                // 2. 풀 초기화
                Queue<GameObject> objectPool = new();

                for (int i = 0; i < resourcePool.size; i++)
                {
                    GameObject obj = Instantiate(resourcePool.prefab, parentObj.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                resourcePoolDictionary.Add(resourcePool.tag, objectPool);
            }
        }
    }

    void ZombiePooling()
    {
        zombiePoolDictionary = new Dictionary<string, Queue<GameObject>>();
        poolParents = new Dictionary<string, Transform>();

        foreach (ZombiePoolGroup zombiePoolGroup in zombiePoolGroups)
        {

            foreach (Pool zombiePool in zombiePoolGroup.zombiePools)
            {
                // 1. 각 리소스 타입별 부모 생성
                GameObject parentObj = new GameObject($"Zombie({zombiePool.tag})");
                parentObj.transform.SetParent(WaveManager.Instance.transform); // ObjectPool 아래 정리
                poolParents[zombiePool.tag] = parentObj.transform;

                // 2. 풀 초기화
                Queue<GameObject> objectPool = new();

                for (int i = 0; i < zombiePool.size; i++)
                {
                    GameObject obj = Instantiate(zombiePool.prefab, parentObj.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                zombiePoolDictionary.Add(zombiePool.tag, objectPool);
            }
        }
    }


    public GameObject SpawnFromResourcePool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!resourcePoolDictionary.ContainsKey(tag)) return null;

        GameObject obj = resourcePoolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        resourcePoolDictionary[tag].Enqueue(obj);
        return obj;
    }
    
    public GameObject SpawnFromZombiePool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!zombiePoolDictionary.ContainsKey(tag)) return null;

        GameObject obj = zombiePoolDictionary[tag].Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        zombiePoolDictionary[tag].Enqueue(obj);
        return obj;
    }
}
