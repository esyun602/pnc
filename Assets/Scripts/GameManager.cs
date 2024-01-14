using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isInGame;
    private bool isInOverRoutine;
    private bool isPaused = false;
    private const float overRoutineTime = 2f;
    private float timePassed;
    private float gameOverTime;
    public const float TimeLimit = 60f;
    public float TimePassed => timePassed;
    public float LeftTime => TimeLimit - timePassed;
    public bool IsInGame => isInGame;
    public int winner {get; set;}
    public GameObject pausePanel;

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

    public void GameOver(bool panCakeWin)
    {
        isInGame = false;
        isInOverRoutine = true;
        gameOverTime = Time.time;
        Time.timeScale = 0.5f;

        if (panCakeWin)
        {
            winner = 1;
        }
        else
        {
            winner = 0;
        }
    }

    public void GameStart()
    {
        isInGame = true;
        timePassed = 0f;
    }

    private void Update()
    {
        if(isPaused)
        {
            IsPaused();
            return;
        }

        if (isInGame && !isPaused)
        {
            timePassed += Time.deltaTime;
            if(LeftTime <= 0)
            {
                timePassed = TimeLimit;
                GameOver(true);
            }
            IsPaused();
        }

        if (isInOverRoutine)
        {
            if (Time.time - gameOverTime > overRoutineTime)
            {
                Time.timeScale = 1f;
                isInOverRoutine = false;
                SceneManager.LoadScene("Clear");
            }
        }

        if((int)timePassed >= 20 && (int)timePassed % 20 == 0)
        {
            SoundManager.Instance.Play_EffectSound(SoundManager.Instance.angry, 0f);
        }
    }

    private void IsPaused()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && pausePanel)
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
