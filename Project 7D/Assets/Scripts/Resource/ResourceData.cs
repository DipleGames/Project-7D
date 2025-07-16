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
    public Sprite icon;
    public string displayName;
    public AudioClip hitSound;
}
