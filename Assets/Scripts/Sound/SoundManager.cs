using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] audioclips;
    AudioSource audioSourceFX;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSourceFX = GetComponent<AudioSource>();
    }


    public void PlaySound(int index)
    {
        audioSourceFX.PlayOneShot(audioclips[index]);
    }
}
