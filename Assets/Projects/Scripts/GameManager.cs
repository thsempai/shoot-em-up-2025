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

    private int playerLife;

    private LifeViewer lifeViewer;

    public void Initialize(Spawner spawner, float cooldown, PlayerControler player, int playerLife, LifeViewer lifeViewer)
    {
        this.spawner = spawner;
        this.cooldown = cooldown;
        this.player = player;
        this.playerLife = playerLife;
        this.lifeViewer = lifeViewer;
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

    public void PlayerContact(GameObject other)
    {

        if (other.TryGetComponent(out EnemyBehavior enemy))
        {
            playerLife -= 1;
            if (playerLife >= 0) lifeViewer.UpdateImages(playerLife);
            EnemyLeaveGame(enemy);
        }
    }
}
