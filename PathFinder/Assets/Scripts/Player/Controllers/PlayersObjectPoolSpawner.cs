using System.Collections.Generic;
using UnityEngine;

public class PlayersObjectPoolSpawner
{
    private PlayerObjectPool playerObjectPool;
    private PlayerConfig playerConfig;
    private SpawnConfig spawnConfig;

    public PlayersObjectPoolSpawner(PlayerConfig playerConfig, SpawnConfig spawnConfig)
    {
        this.playerConfig = playerConfig;
        this.spawnConfig = spawnConfig;

        playerObjectPool = new(playerConfig.PlayerPrefab);
    }

    public List<Player> Spawn()
    {
        List<Player> players = new List<Player>(spawnConfig.PlayerCount);

        for (int i = 0; i < spawnConfig.PlayerCount; i++)
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
                    spawnConfig.ObstaclesLayers
                );

                player.transform.position = randomPosition;

            } while (!positionIsValid);

            players.Add(player);
        }

        return players;
    }
}