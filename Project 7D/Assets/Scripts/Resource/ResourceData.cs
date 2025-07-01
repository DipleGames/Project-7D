using UnityEngine;

public enum ResourceType
{
    Tree,
    Rock,
    Iron,
    Cactus,
    Aloe,
}


[CreateAssetMenu(fileName = "New ResourceData", menuName = "Game/Resource Data")]
public class ResourceData : ScriptableObject
{
    public ResourceType resourceType;
    public Category category;
    public string displayName;
    public float gatherTime = 3f; // 캐는시간
    public Sprite icon;
    public int amount;
    public AudioClip hitSound;
}
