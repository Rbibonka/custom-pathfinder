using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnConfig", menuName = "PathFinder/Spawn Config")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField]
    public Level Level { get; private set; }
}