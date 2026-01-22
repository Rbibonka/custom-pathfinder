using System.Collections.Generic;
using UnityEngine;

namespace PathFind
{
    public class PathfinderAStar
    {
        private PathFinderGrid grid;

        public PathfinderAStar(PathFinderGrid grid)
        {
            this.grid = grid;
        }

        public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
        {
            if (grid == null || grid.Grid == null)
            {
                Debug.LogError("NavigationGrid is not assigned or not initialized");
                return null;
            }

            grid.ResetNodes();

            GridNode startNode = grid.NodeFromWorldPoint(startPos);
            GridNode targetNode = grid.NodeFromWorldPoint(targetPos);

            if (!startNode.Walkable || !targetNode.Walkable)
            {
                Debug.LogWarning("Start or Target node is not walkable");
                return null;
            }

            List<GridNode> openSet = new List<GridNode>();
            HashSet<GridNode> closedSet = new HashSet<GridNode>();

            startNode.SetNode(0, GetDistance(startNode, targetNode), null);

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                GridNode currentNode = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    GridNode candidate = openSet[i];
                    if (candidate.FCost < currentNode.FCost ||
                        (candidate.FCost == currentNode.FCost && candidate.HCost < currentNode.HCost))
                    {
                        currentNode = candidate;
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }

                foreach (GridNode neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                        continue;

                    int newCost = currentNode.GCost + GetDistance(currentNode, neighbour);

                    if (newCost < neighbour.GCost)
                    {
                        neighbour.SetNode(newCost, GetDistance(neighbour, targetNode), currentNode);

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            Debug.LogWarning("Path not found");
            return null;
        }

        private List<Vector3> RetracePath(GridNode startNode, GridNode endNode)
        {
            List<Vector3> path = new List<Vector3>();
            GridNode currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.WorldPosition);
                currentNode = currentNode.Parent;

                if (currentNode == null)
                {
                    Debug.LogError("Broken path: parent is null");
                    return null;
                }
            }

            path.Reverse();
            return path;
        }

        private int GetDistance(GridNode a, GridNode b)
        {
            int dstX = Mathf.Abs(a.GridX - b.GridX);
            int dstY = Mathf.Abs(a.GridY - b.GridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);

            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}