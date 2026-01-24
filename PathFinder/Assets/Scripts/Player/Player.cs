using UnityEngine;

public class Player : PoolableObject
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LayerMask groundMask;

    private MoverByPoints moverByPoints;
    private PlayerConfig playerConfig;

    private PlayerGroundChecker playerGroundChecker;
    private PlayerView playerView;

    private bool canMove;

    public void Initialize(PlayerConfig playerConfig)
    {
        this.playerConfig = playerConfig;

        moverByPoints = new(transform, playerConfig);
        playerGroundChecker = new(transform, groundMask);
        playerView = new(animator);
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

        var currentSurface = playerGroundChecker.GetCurrentPlace();

        if (currentSurface == Surfces.Swamp)
        {
            playerView.ViewSwampWalk();
        }
        else if (currentSurface == Surfces.Road)
        {
            playerView.ViewWalk();
        }
        else
        {
            Debug.LogError("Wrong road");
        }

        if (!moverByPoints.TryMove(currentSurface))
        {
            playerView.ViewIdle();
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, playerConfig.AvoidanceRadius);
    //}
}