using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDrop : MonoBehaviour
{
    private float speed = 15f;
    private static ObjectPool syrupPool;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DummyHitBox>() == null && collision.gameObject.GetComponent<SyrupArea>() == null)
        {
            GetComponent<PooledObject>().Dispose();
            var controller = collision.gameObject.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.Damage();
                return;
            }
            var obj = syrupPool.Instantiate();
            obj.transform.position = transform.position;
        }
    }

    public static void SetSyrupPool(ObjectPool syrupPool)
    {
        DummyDrop.syrupPool = syrupPool;
    }
}
