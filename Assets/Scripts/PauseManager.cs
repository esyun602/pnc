using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameStarter gameStarter;
    public void Resume()
    {
        GameManager.Instance.Pause();
    }

    public void Option()
    {
        optionPanel.SetActive(true);
    }

    public void BackToMain()
    {
        gameStarter.gameObject.SetActive(true);
        gameStarter.InitialSetting();
        GameManager.Instance.Pause();
        GameManager.Instance.ResetTime();
        SceneManager.LoadScene("Title");
    }
}
