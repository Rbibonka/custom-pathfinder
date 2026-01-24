using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum Surfces
{
    None,
    Swamp,
    Road
}

public class PlayerGroundChecker
{
    private const float SphereRadius = 0.4f;
    private const float SphereOffsetY = 0.1f;

    private const string Swamp = nameof(Swamp);
    private const string Road = nameof(Road);

    private LayerMask groundMask;
    private Transform transform;

    public PlayerGroundChecker(Transform transform, LayerMask groundMask)
    {
        this.transform = transform;
        this.groundMask = groundMask;
    }

    public Surfces GetCurrentPlace()
    {
        Vector3 sphereCenter = transform.position + Vector3.down * SphereOffsetY;

        Collider[] colliders = Physics.OverlapSphere(
            sphereCenter,
            SphereRadius,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        foreach (Collider col in colliders)
        {
            if (col.CompareTag(Swamp))
            {
                return Surfces.Swamp;
            }
            else if (col.CompareTag(Road))
            {
                return Surfces.Road;
            }
        }

        return Surfces.None;
    }
}