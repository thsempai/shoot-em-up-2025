using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private EnemiesSpawner enemiesSpawner;
    private BulletsSpawner bulletsSpawner;
    private float bulletDecal = 0.5f;
    private float bulletSpeed;
    private PlayerControler player;
    private List<EnemyBehavior> enemies = new();
    private List<BulletBehavior> bullets = new();
    private float cooldown;
    private float chrono = 0f;

    private int playerLife;

    private LifeViewer lifeViewer;

    public void Initialize(EnemiesSpawner enemiesSpawner, BulletsSpawner bulletsSpawner, float bulletDecal, float bulletSpeed, float cooldown, PlayerControler player, int playerLife, LifeViewer lifeViewer)
    {
        this.enemiesSpawner = enemiesSpawner;
        this.bulletsSpawner = bulletsSpawner;
        this.cooldown = cooldown;
        this.player = player;
        this.playerLife = playerLife;
        this.lifeViewer = lifeViewer;
        this.bulletDecal = bulletDecal;
        this.bulletSpeed = bulletSpeed;
    }

    private void Update()
    {
        chrono += Time.deltaTime;
        if (chrono >= cooldown)
        {
            chrono = 0f;
            EnemyBehavior enemy = enemiesSpawner.Spawn();
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

        for (int index = 0; index < bullets.Count; index++)
        {
            bullets[index].Process();
        }

        player.Process();
    }

    public void AddBullet(Vector3 direction)
    {
        BulletBehavior bullet = bulletsSpawner.Spawn(player.transform, direction, bulletDecal);
        bullet.Initialize(bulletSpeed, this);
        bullets.Add(bullet);
    }

    public void BulletLeaveGame(BulletBehavior bulletBehavior)
    {
        bullets.Remove(bulletBehavior);
        Destroy(bulletBehavior.gameObject);
    }

    public void EnemyLeaveGame(EnemyBehavior enemy)
    {
        enemiesSpawner.DeSpawn(enemy);
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
