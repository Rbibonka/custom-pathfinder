using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] PlayerSpawnPoints { get; private set; }
}