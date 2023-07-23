using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForkHandController : MonoBehaviour, IHandController
{
    // Hand
    [SerializeField]
    private GameObject actionSprite;
    // Arm
    [SerializeField]
    private GameObject normalSprite;
    [SerializeField]
    private Transform leftArm, leftFork;
    [SerializeField]
    private Transform rightArm, rightFork;




    private Vector3 prevPos;
    Vector3 IHandController.UpdatePosition()
    {
        // 팔 천천히 이동
        transform.position = Vector3.MoveTowards(transform.position,
                                        new Vector3(Cursor.Instance.WorldPos.x, transform.position.y, 0f), 3f * Time.deltaTime);

        UpdateArm();
        return transform.position;
    }

    private void UpdateArm()
    {
        var viewPortX = Camera.main.WorldToViewportPoint(Cursor.Instance.WorldPos).x;
        // 오른쪽
        if (viewPortX > 0.5f)
        {
            // Phase 3: 양손일 떄는 유지
            if (rightArm.gameObject.activeSelf)
            {
                rightArm.gameObject.SetActive(true); rightFork.gameObject.SetActive(true);
                return;
            }
            rightArm.gameObject.SetActive(true);    rightFork.gameObject.SetActive(true);
            
            leftArm.gameObject.SetActive(false);    leftFork.gameObject.SetActive(false);
        }
        // 왼쪽
        else
        {
            // Phase 3: 양손일 떄는 유지
            if (leftArm.gameObject.activeSelf)
            {
                leftArm.gameObject.SetActive(true); leftFork.gameObject.SetActive(true);
                return;
            }
            leftArm.gameObject.SetActive(true);     leftFork.gameObject.SetActive(true);
            
            rightArm.gameObject.SetActive(false);   rightFork.gameObject.SetActive(false);
        }
    }

    void IHandController.SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    void IHandController.StartAction()
    {
        gameObject.SetActive(true);
    }

    void IHandController.EndAction()
    {
        gameObject.SetActive(false);
    }
}
