using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text textBox;
    [SerializeField]
    private GameObject maplePrefab;

    private float currentMapleTime = 45f;
    private float currentPhaseShiftTime = 40f;
    private const float genHalfWidth = 0.3f;

    void Update()
    {
        textBox.text = string.Format("{0:F2}", GameManager.Instance.LeftTime);

        if(GameManager.Instance.LeftTime < currentMapleTime)
        {
            var randX = Random.Range(0.5f - genHalfWidth, 0.5f + genHalfWidth);
            var genPos = Camera.main.ViewportToWorldPoint(new Vector2(randX, 1));
            genPos.z = 0;
            var maple = Instantiate(maplePrefab);
            maple.transform.position = genPos;

            currentMapleTime -= 15f;
            if(currentMapleTime == 0)
            {
                currentMapleTime = -10000;
            }
        }

        if(GameManager.Instance.LeftTime < currentPhaseShiftTime)
        {
            Chef.Instance.SetNextPhase();
            currentPhaseShiftTime -= 20f;
            if(currentPhaseShiftTime == 0)
            {
                currentPhaseShiftTime = -10000;
            }
        }
    }
}
