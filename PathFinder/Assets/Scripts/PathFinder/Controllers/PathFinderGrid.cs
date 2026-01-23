using System.Collections.Generic;
using UnityEngine;

namespace PathFind
{
    public class PathFinderGrid
    {
        private Vector2 gridWorldSize = new Vector2(10, 10);
        private float nodeRadius = 0.1f;
        private LayerMask obstacleMask;

        public GridNode[,] Grid => grid;

        private GridNode[,] grid;

        private float nodeDiameter;
        private int gridSizeX, gridSizeY;

        private Transform transform;

        public PathFinderGrid(Transform transform, PathFinderConfig pathFinderConfig)
        {
            this.transform = transform;

            gridWorldSize = pathFinderConfig.GridWorldSize;
            nodeRadius = pathFinderConfig.NodeRadius;
            obstacleMask = pathFinderConfig.ObstacleMask;

            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        }

        public void ResetNodes()
        {
            foreach (GridNode node in grid)
            {
                node.ResetNode();
            }
        }

        public void CreateGrid()
        {
            grid = new GridNode[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft =
                transform.position
                - Vector3.right * gridWorldSize.x / 2
                - Vector3.forward * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint =
                        worldBottomLeft
                        + Vector3.right * (x * nodeDiameter + nodeRadius)
                        + Vector3.forward * (y * nodeDiameter + nodeRadius);

                    bool walkable = true;

                    RaycastHit hit;
                    if (Physics.Raycast(worldPoint + Vector3.up * 5f, Vector3.down, out hit, 10f))
                    {
                        if (((1 << hit.collider.gameObject.layer) & obstacleMask) != 0)
                        {
                            walkable = false;
                        }
                    }

                    grid[x, y] = new GridNode(walkable, worldPoint, x, y);
                }
            }
        }

        public GridNode NodeFromWorldPoint(Vector3 worldPosition)
        {
            Vector3 localPos = worldPosition - transform.position;

            float percentX = (localPos.x + gridWorldSize.x / 2f) / gridWorldSize.x;
            float percentY = (localPos.z + gridWorldSize.y / 2f) / gridWorldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.FloorToInt(percentX * gridSizeX);
            int y = Mathf.FloorToInt(percentY * gridSizeY);

            x = Mathf.Clamp(x, 0, gridSizeX - 1);
            y = Mathf.Clamp(y, 0, gridSizeY - 1);

            return grid[x, y];
        }

        public List<GridNode> GetNeighbours(GridNode node)
        {
            List<GridNode> neighbours = new List<GridNode>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if (checkX < 0 || checkX >= gridSizeX ||
                        checkY < 0 || checkY >= gridSizeY)
                        continue;

                    if (x != 0 && y != 0)
                    {
                        if (!grid[node.GridX + x, node.GridY].Walkable ||
                            !grid[node.GridX, node.GridY + y].Walkable)
                            continue;
                    }

                    neighbours.Add(grid[checkX, checkY]);
                }
            }

            return neighbours;
        }
    }
}