using System;
using UnityEngine;


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
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation);
        (Vector3 min, Vector3 max) = cameraManager.GetRightBorderPoints(forwardSpawn);
        spawner.Initialize(enemyPrefab, min, max, batchNumber);
        gameManager.Initialize(spawner, cooldown);
    }

}
