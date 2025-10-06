using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 minPoint;
    private Vector3 maxPoint;
    private Pool<EnemyBehavior> pool;
    public void Initialize(EnemyBehavior enemy, Vector3 minPoint, Vector3 maxPoint, int batchNumber)
    {
        this.minPoint = minPoint;
        this.maxPoint = maxPoint;

        pool = new(enemy.gameObject, batchNumber);
    }

    public EnemyBehavior Spawn()
    {
        float rnd = Random.Range(0f, 1f);
        return pool.Get(Vector3.Lerp(minPoint, maxPoint, rnd), Quaternion.identity);
    }

    public void DeSpawn(EnemyBehavior enemy)
    {
        pool.Add(enemy);
    }
}
