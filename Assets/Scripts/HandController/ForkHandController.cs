using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkHandController : MonoBehaviour, IHandController
{
    // Hand
    [SerializeField]
    private GameObject actionSprite;
    // Arm
    [SerializeField]
    private GameObject normalSprite;

    private Vector3 prevPos;
    Vector3 IHandController.UpdatePosition()
    {
        prevPos = transform.position;

        // 오른쪽으로 움직이면 시계방향으로 왼팔 회전
        if(prevPos.x - transform.position.x < 0)
        {
            normalSprite.transform.Rotate(new Vector3(0f, 0f, -3f) * Time.deltaTime, Space.World);
        }
        // 왼쪽으로 움직이면 반시계방향으로 왼팔 회전
        else if (prevPos.x - transform.position.x > 0)
        {
            normalSprite.transform.Rotate(new Vector3(0f, 0f, 3f) * Time.deltaTime, Space.World);
        }
        return transform.position;
    }

    void IHandController.SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    void IHandController.StartAction()
    {
        normalSprite.SetActive(true);
        actionSprite.SetActive(true);
    }

    void IHandController.EndAction()
    {
        actionSprite.SetActive(false);
        normalSprite.SetActive(false);
    }
}
