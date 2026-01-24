using System.Collections.Generic;
using UnityEngine;

namespace PathFind
{
    public class PathFinderVisualization : MonoBehaviour
    {
        private PathFinderGrid grid;
        private List<Vector3> pathFinderPath;
        private PathFinderConfig pathFinderConfig;

        public void Initialize(PathFinderGrid grid, PathFinderConfig pathFinderConfig)
        {
            this.grid = grid;
            this.pathFinderConfig = pathFinderConfig;
        }

        public void SetPath(List<Vector3> path)
        {
            pathFinderPath = path;
        }

        private void OnDrawGizmos()
        {
            if (grid == null)
            {
                return;
            }

            foreach (GridNode node in grid.Grid)
            {
                Gizmos.color = node.Walkable ? Color.white : Color.red;
                Gizmos.DrawWireSphere(node.WorldPosition, pathFinderConfig.NodeRadius);
            }

            if (pathFinderPath != null)
            {
                Gizmos.color = Color.green;
                foreach (Vector3 pos in pathFinderPath)
                {
                    Gizmos.DrawWireSphere(pos, pathFinderConfig.NodeRadius);
                }
            }
        }
    }
}