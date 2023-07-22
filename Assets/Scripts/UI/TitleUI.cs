using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Sprite startBtn, startClked;
    [SerializeField] private Sprite howToPlayBtn, howToPlayClked;

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

    public void LoadGame()
    {
        SceneManager.LoadScene("PncMain");
    }
}
