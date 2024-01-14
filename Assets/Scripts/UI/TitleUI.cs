using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Sprite startBtn, startClked;
    [SerializeField] private Sprite howToPlayBtn, howToPlayClked;
    [SerializeField] private GameObject howToPlayWin;

    [SerializeField] private GameObject creditWin;
    [SerializeField] private RectTransform left, right;
    bool canMove = false;

    [SerializeField] private GameObject optionWin;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    void Start()
    {
        SetResolution();
        SoundManager.Instance.PlayIngameBGM(false);
        SoundManager.Instance.PlayBGM(true);
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    void Update()
    {
        if(howToPlayWin.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayWin.SetActive(false);
        }
        if (creditWin.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            creditWin.SetActive(false);
        }
        if (optionWin.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            optionWin.SetActive(false);
        }
        if(canMove)
        {
            left.anchoredPosition = Vector3.MoveTowards(left.anchoredPosition, new Vector3(420f, left.anchoredPosition.y, 0), 300f * Time.deltaTime);
            right.anchoredPosition = Vector3.MoveTowards(right.anchoredPosition, new Vector3(-520f, right.anchoredPosition.y, 0), 300f * Time.deltaTime);
        }
    }

    public void ChangeBtnImg(Image btn)
    {
        if (btn.sprite == startBtn)
        {
            btn.sprite = startClked;
        }
        else if (btn.sprite == startClked)
        {
            btn.sprite = startBtn;
        }
        else if (btn.sprite == howToPlayBtn)
        {
            btn.sprite = howToPlayClked;
        }
        else
        {
            btn.sprite = howToPlayBtn;
        }
    }

    public void ClickedExit()
    {
        Application.Quit();
    }

    public void ClickedTitle()
    {
        creditWin.SetActive(true);
        canMove = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("PncMain");
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

    // 해상도 설정
    // 참고 링크: https://giseung.tistory.com/19
    public void SetResolution()
    {
        // 원하는 해상도
        int setWidth = 1920;
        int setHeight = 1080;

        // 기기 해상도
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        // // credit 크기 변경
        // RectTransform creditRect = creditWin.GetComponent<RectTransform>();
        // creditRect.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
