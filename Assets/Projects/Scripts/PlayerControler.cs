using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionInfo))]
public class PlayerControler : MonoBehaviour
{
    private InputActionAsset actions;
    private InputAction moveAction;
    private InputAction targetAction;
    private const string INPUT_ACTION_MAP = "Player";
    private const string INPUT_MOVE_ACTION = "Move";
    private const string INPUT_TARGET_ACTION = "Target";
    private const string INPUT_SHOOT_ACTION = "Shoot";
    private float speed;

    private Camera cam;

    private BulletBehavior bullet;

    private GameManager gameManager;

    public void Initialize(
        Vector3 position, float spawnForward, Camera cam, Quaternion rotation,
        float speed, InputActionAsset actions, BulletBehavior bullet, GameManager gameManager)
    {
        position.z = spawnForward;
        position = cam.ScreenToWorldPoint(position);
        transform.SetLocalPositionAndRotation(position, rotation);

        this.cam = cam;
        this.actions = actions;
        moveAction = actions.FindActionMap(INPUT_ACTION_MAP).FindAction(INPUT_MOVE_ACTION);
        targetAction = actions.FindActionMap(INPUT_ACTION_MAP).FindAction(INPUT_TARGET_ACTION);

        this.speed = speed;
        this.bullet = bullet;

        this.gameManager = gameManager;
    }

    void OnEnable()
    {
        actions.FindActionMap(INPUT_ACTION_MAP).FindAction(INPUT_SHOOT_ACTION).performed += Shoot;
        actions.FindActionMap(INPUT_ACTION_MAP).Enable();
    }

    void Disable()
    {
        actions.FindActionMap(INPUT_ACTION_MAP).Disable();
        actions.FindActionMap(INPUT_ACTION_MAP).FindAction(INPUT_SHOOT_ACTION).performed -= Shoot;
    }

    public void Process()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = speed * Time.deltaTime * moveAction.ReadValue<Vector2>();
        transform.Translate(movement);

        Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);


        screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Screen.height);

        transform.position = cam.ScreenToWorldPoint(screenPosition);
    }

    private void Shoot(InputAction.CallbackContext callbackContext)
    {
        Vector3 targetPosition = targetAction.ReadValue<Vector2>();
        targetPosition.z = transform.position.z - cam.transform.position.z;

        targetPosition = cam.ScreenToWorldPoint(targetPosition);
        Debug.Log(targetPosition.z);

        Vector3 direction = (targetPosition - transform.position).normalized;
        BulletBehavior newBullet = Instantiate(bullet);
        newBullet.Initialize(transform.position, direction, 5f, gameManager);
        gameManager.AddBullet(newBullet);
    }

}
