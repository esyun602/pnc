using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool parentPool;

    public void SetParentPool(ObjectPool parent)
    {
        parentPool = parent;
    }

    public void Dispose()
    {
        parentPool.Return(this);
    }
}
