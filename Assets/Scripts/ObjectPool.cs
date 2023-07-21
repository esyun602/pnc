using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject originalPrefab;
    private Transform parentTransform;
    private Queue<PooledObject> availableQueue;
    private List<PooledObject> allObjectList = new List<PooledObject>();

    public ObjectPool(GameObject originalPrefab, int initialCount = 0)
    {
        availableQueue = new Queue<PooledObject>();
        this.originalPrefab = originalPrefab;
        parentTransform = new GameObject().transform;
        parentTransform.name = originalPrefab.name + "Pool";
        for (int i = 0; i < initialCount; i++)
        {
            var obj = Object.Instantiate(originalPrefab, parentTransform);
            var pooledObj = obj.AddComponent<PooledObject>();
            pooledObj.SetParentPool(this);
            obj.gameObject.SetActive(false);
            availableQueue.Enqueue(pooledObj);
            allObjectList.Add(pooledObj);
        }
    }

    public PooledObject Instantiate()
    {
        PooledObject obj;
        if(availableQueue.Count > 0)
        {
            obj = availableQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Object.Instantiate(originalPrefab, parentTransform).AddComponent<PooledObject>();
            obj.SetParentPool(this);
            allObjectList.Add(obj);
        }
        return obj;
    }

    public void Return(PooledObject pooledObject)
    {
        if (!allObjectList.Contains(pooledObject))
        {
            return;
        }
        pooledObject.gameObject.SetActive(false);
        availableQueue.Enqueue(pooledObject);
    }

}
