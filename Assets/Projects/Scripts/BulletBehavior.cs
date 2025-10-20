using UnityEngine;

public class BulletBehavior : MonoBehaviour, IPoolClient
{
    private Vector3 direction;
    private float speed;

    private GameManager manager;

    public void Arise(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.SetLocalPositionAndRotation(position, rotation);
    }

    public void Fall()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(float speed, GameManager manager)
    {
        this.speed = speed;
        this.manager = manager;
    }

    public void Process()
    {
        Vector3 movement = speed * Time.deltaTime * Vector3.up;
        transform.Translate(movement, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemy))
        {
            manager.EnemyLeaveGame(enemy);
        }
        manager.BulletLeaveGame(this);
    }
}
