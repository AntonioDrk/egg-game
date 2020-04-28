using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public AudioClip laser;
    public AudioClip star;
    public AudioClip bridge;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip eggCrack;
    public AudioClip pickEgg;
    public AudioClip jump;
    public AudioClip buttonClick;
    public AudioClip walk;
    public AudioClip walkOnWood;

    private GameObject manager { get { return GameObject.Find("SoundManager"); } }
    private AudioSource source { get { return manager.GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        manager.AddComponent<AudioSource>();
        source.clip = laser;
        source.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlaySound(AudioClip sound)
    {
        source.volume = 0.75f;
        source.PlayOneShot(sound);
    }
}
