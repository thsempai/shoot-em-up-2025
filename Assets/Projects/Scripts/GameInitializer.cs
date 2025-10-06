using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Vector3 camPosition;
    [SerializeField] Quaternion camRotation;

    [Space]
    [Header("Spawner")]
    [SerializeField] Spawner spawner;
    [SerializeField] float forwardSpawn = 20f;
    [SerializeField] EnemyBehavior enemyPrefab;
    [SerializeField] int batchNumber;
    [SerializeField] float cooldown;

    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Space]
    [Header("Player")]
    [SerializeField] PlayerControler player;
    [SerializeField] InputActionAsset playerInputActionAsset;
    [SerializeField] Vector2 playerPosition;
    [SerializeField] float playerSpeed;


    void Start()
    {
        CreateObjects();
        InitializeObjects();
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        spawner = Instantiate(spawner);
        gameManager = Instantiate(gameManager);
        player = Instantiate(player);
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation);
        (Vector3 min, Vector3 max) = cameraManager.GetRightBorderPoints(forwardSpawn);
        spawner.Initialize(enemyPrefab, min, max, batchNumber);

        player.Initialize(playerPosition, forwardSpawn, cameraManager.Cam, Quaternion.identity, playerSpeed, playerInputActionAsset);
        player.gameObject.SetActive(true);

        gameManager.Initialize(spawner, cooldown, player);


    }

}
