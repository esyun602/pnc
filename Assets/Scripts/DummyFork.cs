using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFork : MonoBehaviour
{
    private PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 팬케이크 생명 하나씩 줄이기
            if((player = collision.gameObject.GetComponent<PlayerController>()) != null)
            {
                
            }
        }
    }
}
