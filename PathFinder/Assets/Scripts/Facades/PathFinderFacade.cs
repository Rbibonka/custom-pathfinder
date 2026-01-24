using PathFind;
using System.Collections.Generic;
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

    public List<Vector3> FindPath(Vector3 startPoint, Vector3 endPoint)
    {
        var path = pathfinderAStar.FindPath(startPoint, endPoint);

        pathFinderVisualization.SetPath(path);

        return path;
    }
}