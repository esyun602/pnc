using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
    private int winner;
    [SerializeField] private GameObject chefWin, pancakeWin;
    [SerializeField] private RectTransform chefResult, pancakeResult, chefWinner, pancakeWinner;
    [SerializeField] private Text timeChef, timePancake;

    private float timePassed;
    private bool escaping;
    void Start()
    {
        SetResolution();

        SoundManager.Instance.PlayIngameBGM(false);
        SoundManager.Instance.PlayBGM(true);

        timePassed = 0f;

        winner = GameManager.Instance.winner;
        // Chef Win
        if (winner == 0)
        {
            timeChef.text = ((int)GameManager.Instance.LeftTime / 60 % 60).ToString("D2")
                            +":"+ ((int)GameManager.Instance.LeftTime % 60).ToString("D2");
            pancakeWin.SetActive(false);
            chefWin.SetActive(true);
        }
        // Pancake Win
        else
        {
            timePancake.text = ((int)GameManager.Instance.LeftTime / 60 % 60).ToString("D2")
                            + ":" + ((int)GameManager.Instance.LeftTime % 60).ToString("D2");
            chefWin.SetActive(false);
            pancakeWin.SetActive(true);
        }

    }
    void Update()
    {
        if (escaping)
        {
            return;
        }

        if (winner == 0)
        {
            chefResult.anchoredPosition = Vector3.MoveTowards(chefResult.anchoredPosition, new Vector3(520f, chefResult.anchoredPosition.y, 0), 300f * Time.deltaTime);
            chefWinner.anchoredPosition = Vector3.MoveTowards(chefWinner.anchoredPosition, new Vector3(-520f, chefWinner.anchoredPosition.y, 0), 300f * Time.deltaTime);

        }
        else
        {
            pancakeResult.anchoredPosition = Vector3.MoveTowards(pancakeResult.anchoredPosition, new Vector3(520f, pancakeResult.anchoredPosition.y, 0), 300f * Time.deltaTime);
            pancakeWinner.anchoredPosition = Vector3.MoveTowards(pancakeWinner.anchoredPosition, new Vector3(-520f, pancakeWinner.anchoredPosition.y, 0), 300f * Time.deltaTime);
        }

        timePassed += Time.deltaTime;
        if(timePassed > 2f && Input.GetKeyDown(KeyCode.Escape))
        {
            escaping = true;
            SceneManager.LoadScene("Title");
        }
    }
    public void ClickedReplay()
    {
        SceneManager.LoadScene("PncMain");
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
    }
}
