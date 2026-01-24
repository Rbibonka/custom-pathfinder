using UnityEngine;

public class MoverByPoints
{
    private Vector3[] points;
    private int currentIndex;

    private const float fixedY = 0.5f;

    private Vector3 smoothedAvoidance;
    private Vector3 avoidanceVelocity;

    private Transform transform;
    private PlayerConfig playerConfig;

    public MoverByPoints(Transform transform, PlayerConfig playerConfig)
    {
        this.transform = transform;
        this.playerConfig = playerConfig;
    }

    public void SetPoints(Vector3[] points)
    {
        this.points = points;
        currentIndex = 0;
    }

    public void Update()
    {
        if (points == null || points.Length == 0)
        {
            return;
        }

        Vector3 currentPos = transform.position;
        currentPos.y = fixedY;

        Vector3 targetPos = points[currentIndex];
        targetPos.y = fixedY;

        Vector3 toTarget = targetPos - currentPos;
        toTarget.y = 0f;

        float distToPoint = toTarget.magnitude;

        if (distToPoint <= playerConfig.PointReachDistance)
        {
            currentIndex++;
            if (currentIndex >= points.Length)
            {
                currentIndex = points.Length - 1;
            }
            return;
        }

        Vector3 moveDir = toTarget.normalized;

        Vector3 avoidance = CalculateAvoidance();

        Vector3 finalDir = moveDir + avoidance;
        finalDir = Vector3.ClampMagnitude(finalDir, 1f);

        Vector3 nextPos = currentPos + finalDir * playerConfig.Speed * Time.deltaTime;
        nextPos.y = fixedY;
        transform.position = nextPos;

        if (finalDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(finalDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                playerConfig.RotationSpeed * Time.deltaTime
            );
        }
    }

    private Vector3 CalculateAvoidance()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            playerConfig.AvoidanceRadius,
            playerConfig.PlayerLayer
        );

        Vector3 avoidance = Vector3.zero;

        foreach (Collider hit in hits)
        {
            if (hit.transform == transform)
            {
                continue;
            }

            Vector3 toOther = hit.transform.position - transform.position;
            toOther.y = 0f;

            float dist = toOther.magnitude;
            if (dist < playerConfig.MinAvoidDistance || dist > playerConfig.AvoidanceRadius)
            {
                continue;

            }

            if (Vector3.Dot(transform.forward, toOther.normalized) < -0.2f)
            {
                continue;
            }

            float weight = 1f - (dist / playerConfig.AvoidanceRadius);
            avoidance += -toOther.normalized * weight;
        }

        Vector3 rawAvoidance = avoidance * playerConfig.AvoidanceForce;

        smoothedAvoidance = Vector3.SmoothDamp(
            smoothedAvoidance,
            rawAvoidance,
            ref avoidanceVelocity,
            playerConfig.AvoidanceSmoothTime
        );

        return smoothedAvoidance;
    }
}