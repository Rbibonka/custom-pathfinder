using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPoolSpawner
{
    private PlayerObjectPool playerObjectPool;
    private PlayerConfig playerConfig;
    private LayerMask obstacleLayer;

    public PlayerObjectPoolSpawner(Player prefab, PlayerConfig playerConfig, LayerMask obstacleLayer)
    {
        this.playerConfig = playerConfig;
        this.obstacleLayer = obstacleLayer;

        playerObjectPool = new(prefab);
    }

    public List<Player> Spawn()
    {
        List<Player> players = new List<Player>(3);

        for (int i = 0; i < 3; i++)
        {
            var player = playerObjectPool.GetFromPool();
            player.Initialize(playerConfig);

            bool positionIsValid;

            do
            {
                var randomPosition = new Vector3(
                    Random.Range(-5f, 5f),
                    0.5f,
                    Random.Range(-5f, 5f)
                );

                positionIsValid = !Physics.CheckSphere(
                    randomPosition,
                    playerConfig.AvoidanceRadius,
                    obstacleLayer
                );

                player.transform.position = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));

            } while (!positionIsValid);

            players.Add(player);
        }

        return players;
    }

    public void Despawn(Player player)
    {
        playerObjectPool.SetToPool(player);
    }
}