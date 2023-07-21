using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text readyTime;
    [SerializeField] private Text min;
    [SerializeField] private Text col;
    [SerializeField] private Text sec;
    private bool ready = false;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= 3f && !ready)
        {
            readyTime.text = (3 - (int)time).ToString();
        }
        else if (time >= 3f && !ready)
        {
            time = 0f;
            ready = true;
            readyTime.gameObject.SetActive(false);
        }
        else
        {
            min.gameObject.SetActive(true);
            col.gameObject.SetActive(true);
            sec.gameObject.SetActive(true);
            min.text = ((int)time / 60%60).ToString("D2");
            sec.text = ((int)time % 60).ToString("D2");
        }
    }
}
