using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    [SerializeField] private AudioMixer mixer;

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

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    public GameObject optionPanel;

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
    private void Start() {

        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1f);
        }

        if (effectSlider != null)
        {
            effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1f);
        }

        if (masterSlider != null)
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        }
    }

    // control sound volume
    public void ControlVolume(string category, float val)
    {
        mixer.SetFloat(category, Mathf.Clamp(Mathf.Log10(val) * 20, -80, 20));
        PlayerPrefs.SetFloat(category, val);
    }

    // master volume
    private bool isMaster = true;
    private float masterLevel = 0f;
    public void SetMasterLevel(float sliderValue)
    {
        masterLevel = sliderValue;
        if (isMaster)
        {
            SoundManager.Instance.ControlVolume("MasterVolume", masterLevel);
        }
    }
    public void TurnMasterLevel(bool isActive)
    {
        if (isActive)
        {
            isMaster = true;
            SoundManager.Instance.ControlVolume("MasterVolume", masterLevel);
        }
        else
        {
            isMaster = false;
            SoundManager.Instance.ControlVolume("MasterVolume", 0f);
        }
    }

    // 배경음악 볼륨 조절
    private bool isBgm = true;
    private float bgmLevel = 0f;
    public void SetBGMLevel(float sliderValue)
    {
        bgmLevel = sliderValue;
        if (isBgm)
        {
            SoundManager.Instance.ControlVolume("BgmVolume", bgmLevel);
        }
    }
    public void TurnBgmLevel(bool isActive)
    {
        if (isActive)
        {
            isBgm = true;
            SoundManager.Instance.ControlVolume("BgmVolume", bgmLevel);
        }
        else
        {
            isBgm = false;
            SoundManager.Instance.ControlVolume("BgmVolume", 0f);
        }
    }

    // 효과음 볼륨 조절
    private bool isEffect = true;
    private float effectLevel = 0f;
    public void SetEffectLevel(float sliderValue)
    {
        effectLevel = sliderValue;
        if (isEffect)
        {
            SoundManager.Instance.ControlVolume("EffectVolume", effectLevel);
        }
    }
    public void TurnEffectLevel(bool isActive)
    {
        if (isActive)
        {
            isEffect = true;
            SoundManager.Instance.ControlVolume("EffectVolume", effectLevel);
        }
        else
        {
            isEffect = false;
            SoundManager.Instance.ControlVolume("EffectVolume", 0f);
        }
    }
    
    public void PlayBGM(bool stop)
    {
        if(stop)
        {
            BGM.Play();
        }
        else
        {
            BGM.Pause();
        }
    }

    public void PlayIngameBGM(bool stop)
    {
        if (stop)
        {
            IngameBGM.Play();
        }
        else
        {
            IngameBGM.Stop();
        }
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
