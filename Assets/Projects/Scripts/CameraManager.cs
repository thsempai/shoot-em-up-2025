using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{

    public Camera Cam{ get; private set; }
    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        Cam = GetComponent<Camera>();
    }

    public (Vector3, Vector3) GetRightBorderPoints(float z)
    {
        Vector3 top = Cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
        Vector3 bottom = Cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, z));
        return (bottom, top);
    }
}
