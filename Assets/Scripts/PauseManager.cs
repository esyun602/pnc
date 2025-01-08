using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
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
        SoundManager.Instance.optionPanel.SetActive(true);
    }
    
    void Update()
    {
        if (SoundManager.Instance.optionPanel == null)
        {
            return;
        }

        if (SoundManager.Instance.optionPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.optionPanel.SetActive(false);
        }
    }
}
