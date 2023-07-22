using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
    public int winner {get; set;}
    [SerializeField] private GameObject chefWin, pancakeWin;
    [SerializeField] private RectTransform chefResult, pancakeResult, chefWinner, pancakeWinner;
    Vector3 minus, plus;
    void Start()
    {
        winner = 0;
        if (winner == 0)
        {
            pancakeWin.SetActive(false);
            chefWin.SetActive(true);
        }
        else
        {
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
