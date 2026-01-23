using UnityEngine;

public class Player : PoolableObject
{
    private MoverByPoints moverByPoints;
    private PlayerConfig playerConfig;

    private bool canMove;

    public void Initialize(PlayerConfig playerConfig)
    {
        this.playerConfig = playerConfig;

        moverByPoints = new(transform, playerConfig);
    }

    public void SetMovePoints(Vector3[] points)
    {
        moverByPoints.SetPoints(points);
        canMove = true;
    }

    private void Update()
    {
        if (moverByPoints == null
            || !canMove)
        {
            return;
        }

        moverByPoints.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerConfig.AvoidanceRadius);
    }
}