using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandController : MonoBehaviour, IHandController
{
    [SerializeField]
    private GameObject actionSprite;
    [SerializeField]
    private GameObject normalSprite;

    void IHandController.UpdatePosition()
    {
        if (normalSprite.activeSelf)
        {
            transform.position = Cursor.Instance.WorldPos;
        }
        else
        {
            transform.position = new Vector2(Cursor.Instance.WorldPos.x, transform.position.y);
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
