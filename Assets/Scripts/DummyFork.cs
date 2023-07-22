using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFork : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.Damage();
        }
    }
}
