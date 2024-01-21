using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameStarter gameStarter;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
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
    
    // master volume
    public void SetMasterLevel(float sliderValue)
    {
        SoundManager.Instance.ControlVolume("MasterVolume", sliderValue);
    }

    // 배경음악 볼륨 조절
    public void SetBGMLevel(float sliderValue)
    {
        SoundManager.Instance.ControlVolume("BgmVolume", sliderValue);
    }

    // 효과음 볼륨 조절
    public void SetEffectLevel(float sliderValue)
    {
        SoundManager.Instance.ControlVolume("EffectVolume", sliderValue);
    }

    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }
    
    void Update()
    {
        if (optionPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(false);
        }
    }
}
