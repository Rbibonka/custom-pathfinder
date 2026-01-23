using PathFind;
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
    private PathFinderVisualization pathFinderVisualization;

    [SerializeField]
    private Transform[] transforms;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private PlayerConfig playerConfig;

    private PathFinderFacade pathFinderFacade;

    private PlayerObjectPoolSpawner playersSpawner;

    private void Awake()
    {
        pathFinderFacade = new(pathFinderGridTransform, pathFinderVisualization, pathFinderConfig);
        pathFinderFacade.CreateGrid();

        playersSpawner = new(playerPrefab, playerConfig);
        var players = playersSpawner.Spawn();

        foreach (var player in players)
        {
            var r = Random.Range(0, transforms.Length - 1);

            var path = pathFinderFacade.FindPath(player.transform.position, transforms[r].position);

            player.SetMovePoints(path.ToArray());
        }
    }

    [ContextMenu("dasdsadas")]
    private void Awawawaw()
    {
        var path = pathFinderFacade.FindPath(transforms[0].position, transforms[1].position);

        //playerWaypointMover.SetPoints(path.ToArray());
    }
}