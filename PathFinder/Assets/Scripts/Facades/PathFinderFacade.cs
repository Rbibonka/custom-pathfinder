using PathFind;
using UnityEngine;

public class PathFinderFacade
{
    private PathfinderAStar pathfinderAStar;
    private PathFinderGrid pathFinderGrid;
    private PathFinderConfig pathFinderConfig;
    private PathFinderVisualization pathFinderVisualization;

    public PathFinderFacade(
        Transform pathFinderGridTransform,
        PathFinderVisualization pathFinderVisualization,
        PathFinderConfig pathFinderConfig)
    {
        this.pathFinderConfig = pathFinderConfig;
        this.pathFinderVisualization = pathFinderVisualization;

        pathFinderGrid = new(pathFinderGridTransform, pathFinderConfig);
        pathfinderAStar = new(pathFinderGrid);
    }

    public void CreateGrid()
    {
        pathFinderGrid.CreateGrid();
        pathFinderVisualization.Initialize(pathFinderGrid, pathFinderConfig);
    }

    public void FindPath(Vector3 startPoint, Vector3 endPoint)
    {
        pathFinderVisualization.SetPath(pathfinderAStar.FindPath(startPoint, endPoint));
    }
}