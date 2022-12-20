using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-20, 20), 4, Random.Range(-20, 20));
    }

    public static Vector3 GetUniqueSpawnPoint(NetworkRunner runner, PlayerRef player)
    {
        return new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
    }
}
