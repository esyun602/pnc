using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: generate in Chief
public class Cursor : MonoBehaviour
{
    public static Cursor Instance { get; private set; }

    public Vector2 WorldPos => transform.position;
    public Vector2 ScreenPos => Camera.main.ScreenToWorldPoint(transform.position);
    public Vector2 ViewPortPos => Camera.main.WorldToViewportPoint(WorldPos);

    private Vector3 currentMoveVector;

    //TODO: param ºÐ¸®
    private float customBottom = 0.1f;
    private float customTop = 1f;
    private float customLeft = 0f;
    private float customRight = 1f;

    public const float Speed = 5f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!GameManager.Instance.IsInGame)
        {
            return;
        }

        DetermineMoveVector();
        ProcessMove();
        
    }

    private void DetermineMoveVector()
    {
        currentMoveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentMoveVector += Vector3.up;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentMoveVector += Vector3.down;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentMoveVector += Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentMoveVector += Vector3.right;
        }

        currentMoveVector = currentMoveVector.normalized;
    }

    private void ProcessMove()
    {
        var targetPos = transform.position + currentMoveVector * Speed * Time.deltaTime;
        transform.position = ModifyPosToCameraGrid(targetPos);
    }

    //TODO: seperation
    private Vector2 ModifyPosToCameraGrid(Vector2 checkPosition)
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(checkPosition);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, customLeft, customRight);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, customBottom, customTop);

        return Camera.main.ViewportToWorldPoint(viewportPosition);
    }
}
