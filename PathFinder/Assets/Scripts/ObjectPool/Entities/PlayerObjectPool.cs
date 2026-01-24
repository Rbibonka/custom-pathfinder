using UnityEngine;

public class PlayerObjectPool : BaseObjectPool<Player>
{
    public PlayerObjectPool(Player prefab, Transform objectsParent = null) : base(prefab, objectsParent) { }
}