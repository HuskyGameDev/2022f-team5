using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;
    public AudioClip[] musicList;

    void Start()
    {
        music.clip = musicList[1];
        music.loop = true;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
