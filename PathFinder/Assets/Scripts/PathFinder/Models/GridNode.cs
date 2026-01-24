using UnityEngine;

namespace PathFind
{
    public class GridNode
    {
        public bool Walkable { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public int GridX { get; private set; }
        public int GridY { get; private set; }

        public int GCost { get; private set; }
        public int HCost { get; private set; }
        public GridNode Parent { get; private set; }

        public int FCost => GCost + HCost;

        public GridNode(bool walkable, Vector3 worldPos, int x, int y)
        {
            Walkable = walkable;
            WorldPosition = worldPos;
            GridX = x;
            GridY = y;
        }

        public void SetNode(int gCost, int hCost, GridNode parent)
        {
            GCost = gCost;
            HCost = hCost;
            Parent = parent;
        }

        public void ResetNode()
        {
            GCost = int.MaxValue;
            HCost = 0;
            Parent = null;
        }
    }
}