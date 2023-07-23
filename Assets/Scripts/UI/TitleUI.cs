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
    Vector3 minus, plus;
    bool canMove = false;

    void Start()
    {
        SoundManager.Instance.PlayIngameBGM(false);
        SoundManager.Instance.PlayBGM(true);
        minus = Camera.main.WorldToScreenPoint(new Vector3(-5f, 0, 0));
        plus = Camera.main.WorldToScreenPoint(new Vector3(4f, 0, 0));
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
        if(canMove)
        {
            left.position = Vector3.MoveTowards(left.position, new Vector3(minus.x, left.position.y, 0), 300f * Time.deltaTime);
            right.position = Vector3.MoveTowards(right.position, new Vector3(plus.x, right.position.y, 0), 300f * Time.deltaTime);
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
}
