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

    private float offset;



    private Vector3 prevPos;
    Vector3 IHandController.UpdatePosition()
    {
        // 팔 천천히 이동
        transform.position = Vector3.MoveTowards(transform.position,
                                        new Vector3(Cursor.Instance.WorldPos.x + offset, transform.position.y, 0f), 3f * Time.deltaTime);

        UpdateArm();
        return transform.position;
    }

    private void UpdateArm()
    {
        var viewPortX = Camera.main.WorldToViewportPoint(Cursor.Instance.WorldPos).x;
        // 오른쪽
        if (viewPortX > 0.5f)
        {
            rightArm.gameObject.SetActive(true);    rightFork.gameObject.SetActive(true);
            leftArm.gameObject.SetActive(false);    leftFork.gameObject.SetActive(false);
            offset = 0;
        }
        // 왼쪽
        else
        {
            leftArm.gameObject.SetActive(true);     leftFork.gameObject.SetActive(true);
            rightArm.gameObject.SetActive(false);   rightFork.gameObject.SetActive(false);
            offset = 0;
        }
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
