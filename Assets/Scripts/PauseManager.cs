using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameStarter gameStarter;
    public void Resume()
    {
        GameManager.Instance.Pause();
    }

    public void BackToMain()
    {
        gameStarter.gameObject.SetActive(true);
        gameStarter.InitialSetting();
        GameManager.Instance.Pause();
        GameManager.Instance.ResetTime();
        SceneManager.LoadScene("Title");
    }

    public void ClickedOption()
    {
        optionPanel.SetActive(true);
    }

    void Start()
    {
        if(!optionPanel)
        {
            optionPanel = SoundManager.Instance.optionPanel;
        }
    }
    
    void Update()
    {
        if (optionPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(false);
        }
    }
}
