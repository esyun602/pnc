using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManager : MonoBehaviour
{
    [SerializeField]
    private Transform TopLeftMargin;
    [SerializeField]
    private Transform LeftLeftMargin;
    [SerializeField]
    private Transform RightLeftMargin;
    [SerializeField]
    private Transform BottomLeftMargin;


    [SerializeField]
    private Transform TopRightMargin;
    [SerializeField]
    private Transform LeftRightMargin;
    [SerializeField]
    private Transform RightRightMargin;
    [SerializeField]
    private Transform BottomRightMargin;

    [SerializeField]
    private Transform LeftEye;
    [SerializeField]
    private Transform RightEye;

    private void Update()
    {
        LeftEye.position = new Vector2(Mathf.Lerp(LeftLeftMargin.position.x, RightLeftMargin.position.x, Cursor.Instance.ViewPortPos.x), Mathf.Lerp(BottomLeftMargin.position.y, TopLeftMargin.position.y, Cursor.Instance.ViewPortPos.y));
        RightEye.position = new Vector2(Mathf.Lerp(LeftRightMargin.position.x, RightRightMargin.position.x, Cursor.Instance.ViewPortPos.x), Mathf.Lerp(BottomRightMargin.position.y, TopRightMargin.position.y, Cursor.Instance.ViewPortPos.y));
    }
}
