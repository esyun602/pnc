using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupArea : MonoBehaviour
{
    private float timePassed = 0f;
    private const float SyrupDuration = 5f;
    private PooledObject pooledObj;
    private void Start()
    {
        pooledObj = GetComponent<PooledObject>();
    }

    private void OnEnable()
    {
        timePassed = 0f;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > SyrupDuration)
        {
            pooledObj.Dispose();
        }
    }
}
