using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandController : MonoBehaviour, IHandController
{
    [SerializeField]
    private GameObject laserObject;

    private const float maxHeight = 4f;
    private const float minHeight = -2.5f;
    private const float velocity = Cursor.Speed * 4;

    private float lastEndTime;
    private const float restoreTIme = 1f;

    Vector3 IHandController.UpdatePosition()
    {/*
        var targetPos = new Vector3(transform.position.x, Mathf.Min(Cursor.Instance.WorldPos.y, maxHeight));
        var moveVector = targetPos - transform.position;
        var moveMagnitude = velocity * Time.deltaTime;
        if(moveVector.magnitude < moveMagnitude)
        {
            moveMagnitude = moveVector.magnitude;
        }

        transform.position += moveVector.normalized * moveMagnitude;*/
        if (Time.time - lastEndTime < restoreTIme)
        {
            var targetPos = new Vector3(transform.position.x, Mathf.Clamp(Cursor.Instance.WorldPos.y, minHeight, maxHeight));

            var moveVector = targetPos - transform.position;
            var moveMagnitude = velocity * Time.deltaTime;
            if (moveVector.magnitude < moveMagnitude)
            {
                moveMagnitude = moveVector.magnitude;
            }
            transform.position += moveVector.normalized * moveMagnitude;
        }
        else
        {
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(Cursor.Instance.WorldPos.y, minHeight, maxHeight));
        }
        return transform.position;
    }

    void IHandController.SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    void IHandController.StartAction()
    {
        laserObject.SetActive(true);
    }

    void IHandController.EndAction()
    {
        laserObject.SetActive(false);
        lastEndTime = Time.time;
    }
}
