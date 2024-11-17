using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : Singleton<soundManager>
{
    public AudioSource[] SoundAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayOneShot(int index)
    {
        if(index < SoundAudioSource.Length)
        {
            SoundAudioSource[index].PlayOneShot(SoundAudioSource[index].clip);
        }
    }
    public void PlayMusic(int index)
    {
        if(index < SoundAudioSource.Length)
        {
            SoundAudioSource[index].Play();
        }
    }
}
