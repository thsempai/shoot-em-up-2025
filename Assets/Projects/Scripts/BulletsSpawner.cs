using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    private Pool<BulletBehavior> pool;

    public void Initialize(BulletBehavior bullet, int batchNumber)
    {
        pool = new(bullet.gameObject, batchNumber);
    }

    public BulletBehavior Spawn(Transform player, Vector3 direction, float decal)
    {

        BulletBehavior bullet = pool.Get(decal * direction + player.position, Quaternion.identity);
        bullet.transform.up = direction;
        return bullet;
    }

    public void DeSpawn(BulletBehavior bullet)
    {
        pool.Add(bullet);
    }
}
