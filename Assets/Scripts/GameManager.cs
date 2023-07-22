using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isInGame;
    private float timePassed;

    public bool IsInGame => isInGame;
    public float TimePassed => timePassed;

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
            timePassed += Time.deltaTime;
        }
    }
}
