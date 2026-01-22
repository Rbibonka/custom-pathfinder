using UnityEngine;

[CreateAssetMenu(fileName = "NewPathFinderConfig", menuName = "PathFinder/PathFinderGrid")]
public class PathFinderConfig : ScriptableObject
{
    [field: SerializeField]
    public Vector2 GridWorldSize { get; private set; }

    [field: SerializeField]
    public float NodeRadius { get; private set; }

    [field: SerializeField]
    public LayerMask ObstacleMask { get; private set; }
}