using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    private InputActionAsset actions;
    private InputAction moveAction;
    private const string INPUT_ACTION_MAP = "Player";
    private const string INPUT_ACTION = "Move";
    private float speed;

    private Camera cam;

    public void Initialize(Vector3 position, float spawnForward, Camera cam, Quaternion rotation, float speed, InputActionAsset actions)
    {
        position.z = spawnForward;
        position = cam.ScreenToWorldPoint(position);
        transform.SetLocalPositionAndRotation(position, rotation);

        this.cam = cam;
        this.actions = actions;
        moveAction = actions.FindActionMap(INPUT_ACTION_MAP).FindAction(INPUT_ACTION);

        this.speed = speed;
    }

    void OnEnable()
    {
        actions.FindActionMap(INPUT_ACTION_MAP).Enable();
    }

    void Disable()
    {
        actions.FindActionMap(INPUT_ACTION_MAP).Disable();
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


}
