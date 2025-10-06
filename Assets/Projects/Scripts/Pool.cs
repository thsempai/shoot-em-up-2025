using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pool<T>
where T : IPoolClient
{
    private GameObject prefab;
    private int batchNumber;
    private Queue<T> queue = new();
    public Pool(GameObject prefab, int batchNumber)
    {
        if (prefab.GetComponent<IPoolClient>() == null)
        {
            throw new System.ArgumentException("Prefab doesn't have a componenent that have implement IPoolClient");
        }

        this.prefab = prefab;
        this.batchNumber = batchNumber;

        CreateBatch();
    }

    private void CreateBatch()
    {
        for (int _ = 0; _ < batchNumber; _++)
        {
            GameObject go = Object.Instantiate(prefab);
            T client = go.GetComponent<T>();
            Add(client);
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        if (queue.Count == 0) CreateBatch();
        T client = queue.Dequeue();
        client.Arise(position, rotation);
        return client;
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Fall();
    }
}
