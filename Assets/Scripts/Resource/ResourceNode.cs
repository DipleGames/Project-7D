using TMPro;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ResourceData Data;
    public ResourceType ResourceType => Data.resourceType;
    public float gatherTime = 3f; // 캐는시간
    public int getAmount;
}
