using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dummy
public class DummyHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DummyHitBox>() == null)
        {
            gameObject.SetActive(false);
        }
    }
}
