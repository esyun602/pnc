using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    private Text text;
    public float timePassed = 0;
    public float runTime = 3f;

    public void InitialSetting()
    {
        SoundManager.Instance.PlayBGM(false);
        SoundManager.Instance.PlayIngameBGM(true);

        text = GetComponent<Text>();
        timePassed = 0f;
        runTime = 3f;
        text.text = string.Format("{0:F0}", runTime);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitialSetting();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 1f)
        {
            timePassed = 0f;
            runTime -= 1;
            if(runTime == 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameStart();
                GameManager.Instance.pausePanel = pause;
            }
            else
            {
                text.text = string.Format("{0:F0}", runTime);
            }
        }
    }
}
