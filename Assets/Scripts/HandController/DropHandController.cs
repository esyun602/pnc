using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandController : MonoBehaviour, IHandController
{
    [SerializeField]
    private GameObject actionSprite;
    [SerializeField]
    private GameObject normalSprite;
    [SerializeField]
    private Transform leftArm;
    [SerializeField]
    private Transform rightArm;

    [SerializeField]
    private Transform leftArmLeftMargin;
    [SerializeField]
    private Transform leftArmRightMargin;
    [SerializeField]
    private Transform rightArmLeftMargin;
    [SerializeField]
    private Transform rightArmRightMargin;

    void IHandController.UpdatePosition()
    {
        //TODO: FIX
        if (normalSprite.activeSelf)
        {
            transform.position = new Vector2(Cursor.Instance.WorldPos.x, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(Cursor.Instance.WorldPos.x, transform.position.y);
        }

        UpdateArm();
    }

    private void UpdateArm()
    {
        var viewPortX = Camera.main.WorldToViewportPoint(transform.position).x;
        if (viewPortX > 0.5f)
        {
            rightArm.gameObject.SetActive(true);
            leftArm.gameObject.SetActive(false);
            rightArm.position = Vector3.Lerp(rightArmLeftMargin.position, rightArmRightMargin.position, viewPortX * 2 - 1);
            rightArm.localRotation = Quaternion.Lerp(rightArmLeftMargin.rotation, rightArmRightMargin.rotation, viewPortX * 2 - 1);
        }
        else
        {
            leftArm.gameObject.SetActive(true);
            rightArm.gameObject.SetActive(false);
            leftArm.position = Vector3.Lerp(leftArmLeftMargin.position, leftArmRightMargin.position, viewPortX * 2);
            leftArm.localRotation = Quaternion.Lerp(leftArmLeftMargin.rotation, leftArmRightMargin.rotation, viewPortX * 2);
        }
    }

    void IHandController.SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    void IHandController.StartAction()
    {
        normalSprite.SetActive(false);
        actionSprite.SetActive(true);
    }

    void IHandController.EndAction()
    {
        actionSprite.SetActive(false);
        normalSprite.SetActive(true);
    }
}
