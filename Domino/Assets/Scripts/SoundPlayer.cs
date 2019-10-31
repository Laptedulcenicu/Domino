using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
   
    public AudioClip button;
    public AudioClip death;
    public AudioClip win;
    public AudioClip domino;

    private AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
    
        if (PlayerPrefs.GetInt("sound", 1) == 0)
            return;

        if (name == "button")
            aSource.PlayOneShot(button);

        if (name == "death")
            aSource.PlayOneShot(death);

        if (name == "win")
            aSource.PlayOneShot(win);

        if (name == "domino")
            aSource.PlayOneShot ( domino );
    }

    public void PlayVibration ( )
    {
        if (PlayerPrefs.GetInt ( "vibration", 1 ) == 0)
            return;
        Handheld.Vibrate ( );

    }
}
