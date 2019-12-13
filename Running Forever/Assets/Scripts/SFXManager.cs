using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // variable that holds the instance for the singleton setup

    public AudioSource music; // Contains the sneaking music audio

    public AudioSource jump;
    public AudioSource doubleJump;
    public AudioSource coinCollect;
    public AudioSource playerPop;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Method for pausing the music, will check which one is playing, pause it and save it in the Enum
    public void PauseMusic()
    {
        music.Pause();
    }

    // Method for playing the music after pausing, will check what the Enum is and play accordingly
    public void PlayMusic()
    {
        music.Play();
    }
}
