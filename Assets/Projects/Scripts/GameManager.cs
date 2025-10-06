using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private PlayerControler player;
    private List<EnemyBehavior> enemies = new();
    private float cooldown;
    private float chrono = 0f;

    public void Initialize(Spawner spawner, float cooldown, PlayerControler player)
    {
        this.spawner = spawner;
        this.cooldown = cooldown;
        this.player = player;
    }

    private void Update()
    {
        chrono += Time.deltaTime;
        if (chrono >= cooldown)
        {
            chrono = 0f;
            EnemyBehavior enemy = spawner.Spawn();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.Initialize(this);
            }
        }

        for (int index = 0; index < enemies.Count; index++)
        {
            enemies[index].Process();
        }

        player.Process();
    }

    public void EnemyLeaveGame(EnemyBehavior enemy)
    {
        spawner.DeSpawn(enemy);
    }
}
