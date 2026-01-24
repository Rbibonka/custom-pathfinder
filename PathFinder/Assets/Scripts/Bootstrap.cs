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
    private SpawnConfig spawnConfig;

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
        pathFinderFacade = new(pathFinderGridTransform, pathFinderVisualization, pathFinderConfig);
        pathFinderFacade.CreateGrid();

        playersSpawner = new(playerConfig, spawnConfig);
        var players = playersSpawner.Spawn();

        GameLoop gameLoop = new(players, mouseClickHandler, pathFinderFacade);
    }
}