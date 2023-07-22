using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLaserHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
