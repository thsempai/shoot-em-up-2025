using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Vector3 camPosition;
    [SerializeField] Quaternion camRotation;

    [Space]
    [Header("Spawner")]
    [SerializeField] EnemiesSpawner enemiesSpawner;
    [SerializeField] float forwardSpawn = 20f;
    [SerializeField] EnemyBehavior enemyPrefab;
    [SerializeField] int enemiesBatchNumber;
    [SerializeField] float cooldown;

    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Space]
    [Header("Player")]
    [SerializeField] int life = 3;
    [SerializeField] PlayerControler player;
    [SerializeField] InputActionAsset playerInputActionAsset;
    [SerializeField] Vector2 playerPosition;
    [SerializeField] float playerSpeed;

    [Space]
    [Header("Player - Bullet")]
    [SerializeField] BulletsSpawner bulletsSpawner;
    [SerializeField] BulletBehavior bulletPrefab;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletSpawnDecal = 0.5f;
    [SerializeField] int bulletsBatchNumber;

    [Space]
    [Header("UI")]
    [SerializeField] LifeViewer lifeCanvas;
    [SerializeField] Image lifeImage;
    [SerializeField] Vector2 firstImagePosition;
    [SerializeField] Vector2 imageOffSet;
    [SerializeField] ScoreViewer scoreViewer;
    [SerializeField] int scoreDigit;
    [SerializeField] Vector2 scorePosition;


    void Start()
    {
        CreateObjects();
        InitializeObjects();
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        enemiesSpawner = Instantiate(enemiesSpawner);
        gameManager = Instantiate(gameManager);
        player = Instantiate(player);
        lifeCanvas = Instantiate(lifeCanvas);
        bulletsSpawner = Instantiate(bulletsSpawner);
        scoreViewer = Instantiate(scoreViewer);
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation);
        (Vector3 min, Vector3 max) = cameraManager.GetRightBorderPoints(forwardSpawn);
        enemiesSpawner.Initialize(enemyPrefab, min, max, enemiesBatchNumber);

        player.Initialize(playerPosition, forwardSpawn, cameraManager.Cam, Quaternion.identity, playerSpeed, playerInputActionAsset, gameManager);
        player.gameObject.SetActive(true);

        player.GetComponent<PlayerCollisionInfo>().Initialize(gameManager);

        gameManager.Initialize(enemiesSpawner, bulletsSpawner, bulletSpawnDecal, bulletSpeed, cooldown, player, life, lifeCanvas, scoreViewer);

        lifeCanvas.Initialize(lifeImage, life, firstImagePosition, imageOffSet);

        bulletsSpawner.Initialize(bulletPrefab, bulletsBatchNumber);

        scoreViewer.Initialize(scoreDigit, lifeCanvas.Canvas, scorePosition);

    }

}
