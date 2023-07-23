using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    // damaged1, 2 : 피격 시 팬케이크가 내는 소리
    [SerializeField] private AudioSource damaged1;
    [SerializeField] private AudioSource damaged2;
    // laugh: 공격 성공 후 요리사가 내는 소리
    [SerializeField] private AudioSource laugh;
    // angry: 20초 지날 때마다 요리사가 내는 소리
    [SerializeField] private AudioSource angry;
    // jump: 점프할 때 나는 소리
    [SerializeField] private AudioSource jump;
    // slam: 포크 찍을 때 나는 소리
    [SerializeField] private AudioSource slam;
    // pewpew2: 시럽 빔 쏘는 소리
    [SerializeField] private AudioSource pewpew2;
    // pewpew9: 공격(종류무관) 받을 때 나는 소리
    [SerializeField] private AudioSource pewpew9;
    // pewpew17: 단풍잎 획득할 때 소리
    [SerializeField] private AudioSource pewpew17;
    // BGM
    [SerializeField] private AudioSource BGM;
    // INGAME BGM
    [SerializeField] private AudioSource IngameBGM;

    public void PancakeDamaged()
    {
        pewpew9.Play();
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            damaged1.Play();
        }
        else
        {
            damaged2.Play();
        }
        Invoke("AttackSucceed", 0.1f);
    }

    public void AttackSucceed()
    {
        laugh.Play();
    }

    public void ChefSound()
    {
        angry.Play();
    }

    public void JumpSound()
    {
        jump.Play();
    }

    public void ForkSound()
    {
        Invoke("SlamSound", 0.5f);
    }
    private void SlamSound()
    {
        slam.Play();
    }

    public void SyrupSound()
    {
        pewpew2.Play();
    }

    public void EarnMaple()
    {
        pewpew17.Play();
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
