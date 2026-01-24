using System.Collections.Generic;
using UnityEngine;

public class PlayersObjectPoolSpawner
{
    private PlayerObjectPool playerObjectPool;
    private PlayerConfig playerConfig;
    private Transform[] spawnPoints;

    public PlayersObjectPoolSpawner(PlayerConfig playerConfig, Transform[] spawnPoints)
    {
        this.playerConfig = playerConfig;
        this.spawnPoints = spawnPoints;

        playerObjectPool = new(playerConfig.PlayerPrefab);
    }

    public List<Player> Spawn()
    {
        List<Player> players = new List<Player>(spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var player = playerObjectPool.GetFromPool();
            player.Initialize(playerConfig);

            player.transform.position = spawnPoints[i].position;
            players.Add(player);
        }

        return players;
    }
}