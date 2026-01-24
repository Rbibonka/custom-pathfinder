using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnConfig", menuName = "PathFinder/Spawn Config")]
public class SpawnConfig : ScriptableObject
{
    [field: SerializeField]
    public int PlayerCount { get; private set; }

    [field: SerializeField]
    public LayerMask ObstaclesLayers { get; private set; }
}