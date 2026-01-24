using PathFind;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private Transform pathFinderGridTransform;

    [SerializeField]
    private PathFinderConfig pathFinderConfig;

    [SerializeField]
    private LevelConfig levelConfig;

    [SerializeField]
    private PathFinderVisualization pathFinderVisualization;

    [SerializeField]
    private PlayerConfig playerConfig;

    [SerializeField]
    private MouseClickHandler mouseClickHandler;

    private PathFinderFacade pathFinderFacade;
    private PlayersObjectPoolSpawner playersSpawner;

    private void Awake()
    {
        var level = Instantiate(levelConfig.Level, Vector3.zero, Quaternion.identity);

        pathFinderFacade = new(level.transform, pathFinderVisualization, pathFinderConfig);
        pathFinderFacade.CreateGrid();

        playersSpawner = new(playerConfig, level.PlayerSpawnPoints);
        var players = playersSpawner.Spawn();

        GameLoop gameLoop = new(players, mouseClickHandler, pathFinderFacade);
    }
}