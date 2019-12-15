using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // variable that holds the instance for the singleton setup

    public AudioSource music; // Contains the sneaking music audio

    public AudioSource jump; // Contains the jumping audio
    public AudioSource doubleJump; // Contains the doubleJumping audio
    public AudioSource coinCollect; // Contains the coinCollecting audio
    public AudioSource playerPop; // Contains the playerPopping audio

    void Awake()
    {
        // Check if the Instance variable is not null and not 'this'
        if (Instance != null && Instance != this)
        {
            // Destroy gameObject connected to this script if Instance is already defined
            Destroy(this.gameObject);
        }
        else
        {
            // Assign 'this' to the Instance variable if Instance is null
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
