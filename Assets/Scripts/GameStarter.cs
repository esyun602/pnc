using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    private Text text;
    private float timePassed = 0;
    private float runTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(false);
        SoundManager.Instance.PlayIngameBGM(true);

        text = GetComponent<Text>();
        timePassed = 0f;
        runTime = 3f;
        text.text = string.Format("{0:F0}", runTime);
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
            }
            else
            {
                text.text = string.Format("{0:F0}", runTime);
            }
        }
    }
}
