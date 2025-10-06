using UnityEngine;

public interface IPoolClient
{
    public void Arise(Vector3 position, Quaternion rotation);
    public void Fall();
}
