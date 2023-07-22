using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    private int winner;
    [SerializeField] private GameObject chefWin, pancakeWin;
    [SerializeField] private RectTransform chefResult, pancakeResult, chefWinner, pancakeWinner;
    [SerializeField] private Text timeChef, timePancake;
    Vector3 minus, plus;
    void Start()
    {
        winner = GameManager.Instance.winner;
        // Chef Win
        if (winner == 0)
        {
            timeChef.text = GameManager.Instance.TimePassed.ToString();
            pancakeWin.SetActive(false);
            chefWin.SetActive(true);
        }
        // Pancake Win
        else
        {
            timePancake.text = GameManager.Instance.TimePassed.ToString();
            chefWin.SetActive(false);
            pancakeWin.SetActive(true);
        }

        minus = Camera.main.WorldToScreenPoint(new Vector3(-5f, 0, 0));
        plus = Camera.main.WorldToScreenPoint(new Vector3(5f, 0, 0));
    }
    void Update()
    {
        if (winner == 0)
        {
            chefResult.position = Vector3.MoveTowards(chefResult.position, new Vector3(minus.x, chefResult.position.y, 0), 300f * Time.deltaTime);
            chefWinner.position = Vector3.MoveTowards(chefWinner.position, new Vector3(plus.x, chefWinner.position.y, 0), 300f * Time.deltaTime);

        }
        else
        {
            pancakeResult.position = Vector3.MoveTowards(pancakeResult.position, new Vector3(minus.x, pancakeResult.position.y, 0), 300f * Time.deltaTime);
            pancakeWinner.position = Vector3.MoveTowards(pancakeWinner.position, new Vector3(plus.x, pancakeWinner.position.y, 0), 300f * Time.deltaTime);
        }
    }
}
