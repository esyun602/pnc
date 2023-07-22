using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isInGame;
    private float timePassed;
    public const float TimeLimit = 60f;
    public float TimePassed => timePassed;
    public float LeftTime => TimeLimit - timePassed;
    public bool IsInGame => isInGame;
    public int winner {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        isInGame = false;
    }

    public void GameStart()
    {
        isInGame = true;
        timePassed = 0f;
    }

    private void Update()
    {
        if (isInGame)
        {
            timePassed = Time.deltaTime;
            if(LeftTime <= 0)
            {
                timePassed = TimeLimit;
                GameOver();
            }
        }
    }
}
