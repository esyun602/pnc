using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : MonoBehaviour
{
    //TODO: pooling
    [SerializeField]
    private DummyHitBox DummyHitBoxPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            var instance = Instantiate(DummyHitBoxPrefab);
            instance.transform.position = Cursor.Instance.WorldPos;
        }
    }

}
