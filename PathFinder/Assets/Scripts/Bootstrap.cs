using PathFind;
using UnityEngine;

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

    private PathFinderFacade pathFinderFacade;

    private void Awake()
    {
        pathFinderFacade = new(pathFinderGridTransform, pathFinderVisualization, pathFinderConfig);

        pathFinderFacade.CreateGrid();
        
    }

    [ContextMenu("dasdsadas")]
    private void Awawawaw()
    {
        pathFinderFacade.FindPath(transforms[0].position, transforms[1].position);
    }
}