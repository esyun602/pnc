using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text textBox;
    void Update()
    {
        textBox.text = string.Format("%.2f", GameManager.Instance.LeftTime);
    }
}
