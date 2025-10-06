using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IPoolClient
{
    public bool Alive => gameObject.activeInHierarchy;
    [SerializeField] Vector3 speed = Vector3.left * 10f;

    private GameManager manager;
    public void Arise(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.SetLocalPositionAndRotation(position, rotation);
    }

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
    }
    public void Fall()
    {
        gameObject.SetActive(false);
    }

    public void Process()
    {
        if (!Alive) return;
        transform.Translate(speed * Time.deltaTime);  
    }

    void OnBecameInvisible()
    {
        manager.EnemyLeaveGame(this);
    }

}
