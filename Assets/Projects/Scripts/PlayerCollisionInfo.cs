using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCollisionInfo : MonoBehaviour
{
    private GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void OnTriggerEnter(Collider other)
    {
        gameManager.PlayerContact(other.gameObject);
    }
}
