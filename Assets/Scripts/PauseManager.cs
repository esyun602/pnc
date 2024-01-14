using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
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
        SceneManager.LoadScene("PncMain");
        SceneManager.LoadScene("Title");
        // 시간 재시작, 값 초기화 필요
    }
}
