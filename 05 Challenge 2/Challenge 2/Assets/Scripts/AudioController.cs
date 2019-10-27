using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip musicClipOne;
    public AudioClip musicClipDone;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        //audio
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
