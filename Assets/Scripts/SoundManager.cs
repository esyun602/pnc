using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [Header("Audio Sources")]
    // damaged1, 2 : 피격 시 팬케이크가 내는 소리
    public AudioSource[] damaged;
    // laugh: 공격 성공 후 요리사가 내는 소리
    public AudioSource laugh;
    // angry: 20초 지날 때마다 요리사가 내는 소리
    public AudioSource angry;
    // jump: 점프할 때 나는 소리
    public AudioSource jump;
    // slam: 포크 찍을 때 나는 소리
    public AudioSource slam;
    // pewpew2: 시럽 빔 쏘는 소리
    public AudioSource pewpew2;
    // pewpew9: 공격(종류무관) 받을 때 나는 소리
    public AudioSource pewpew9;
    // pewpew12: 대쉬할 때 나는 소리
    public AudioSource pewpew12;
    // pewpew17: 단풍잎 획득할 때 소리
    public AudioSource pewpew17;
    // BGM
    public AudioSource BGM;
    // INGAME BGM
    public AudioSource IngameBGM;

    private AudioSource delay_effect;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            
            Destroy(gameObject);
        }
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    
    public void PlayBGM(bool stop)
    {
        if(stop)
        {BGM.Play();}
        else
        {BGM.Pause();}
    }

    public void PlayIngameBGM(bool stop)
    {
        if (stop)
        { IngameBGM.Play(); }
        else
        { IngameBGM.Stop(); }
    }


    public void Play_EffectSound(AudioSource effectSource, float time)
    {
        if(time == 0f)
        {
            effectSource.Play();
        }
        else
        {
            delay_effect = effectSource;
            Invoke("Play_DelayedEffect", time);
        }
    }

    public void Play_EffectSound_Random(AudioSource[] effect, int random)
    {
        int rand = Random.Range(0, random);
        effect[rand].Play();
    }

    private void Play_DelayedEffect()
    {
        delay_effect.Play();
    }
}
