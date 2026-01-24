using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "PathFinder/Player Config")]
public class PlayerConfig : ScriptableObject
{
    [field: Header("Prefab")]
    [field: SerializeField()]
    public Player PlayerPrefab { get; private set; }

    [field: Header("Movement")]
    [field: SerializeField()]
    public float Speed { get; private set; } = 3f;

    [field: SerializeField()]
    public float RotationSpeed { get; private set; }  = 6f;

    [field: SerializeField()]
    public float PointReachDistance { get; private set; } = 0.35f;

    [field: Header("Avoidance")]
    [field: SerializeField()]
    public float AvoidanceRadius { get; private set; } = 1.4f;

    [field: SerializeField()]
    public float AvoidanceForce { get; private set; } = 1.1f;

    [field: SerializeField()]
    public float MinAvoidDistance { get; private set; } = 0.4f;

    [field: SerializeField()]
    public float AvoidanceSmoothTime { get; private set; } = 0.15f;

    [field: SerializeField()]
    public LayerMask PlayerLayer { get; private set; }
}