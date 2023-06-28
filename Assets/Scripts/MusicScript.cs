using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    AudioSource source;
    public AudioClip clip;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        source.clip = clip;
        source.Play();
    }

    public void StopMusic()
    {
        source.mute = true;
    }

    public void PlayMusic()
    {
        source.mute = false;
    }

    void Update()
    {
        
    }
}
