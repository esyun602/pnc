using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandController : MonoBehaviour, IHandController
{
    [SerializeField]
    private GameObject laserObject;
    void IHandController.UpdatePosition()
    {
        transform.position = new Vector2(transform.position.x, Cursor.Instance.WorldPos.y);
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
    }
}
