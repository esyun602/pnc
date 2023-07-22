using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDrop : DummyHitBox
{
    private float speed = 15f;
    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
