using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapleController : MonoBehaviour
{
    private float mapleMoveAccel = -15f;
    private const float roundTime = 1f;
    private float initMapleVelocity => -mapleMoveAccel * roundTime / 2;
    private float currentMapleVelocity;
    private float mapleTimePassed = 0;

    private void OnEnable()
    {
        mapleTimePassed = 0;
        currentMapleVelocity = initMapleVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        mapleTimePassed += Time.deltaTime;
        if (mapleTimePassed > roundTime)
        {
            mapleTimePassed = 0;
            mapleMoveAccel *= -1;
            currentMapleVelocity = initMapleVelocity;
        }
        else
        {
            currentMapleVelocity += mapleMoveAccel * Time.deltaTime;
            transform.position += Vector3.right * (currentMapleVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var controller = collision.GetComponent<PlayerController>();
        if(controller != null)
        {
            controller.IsMapled = true;
        }
        else if(collision.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
}
