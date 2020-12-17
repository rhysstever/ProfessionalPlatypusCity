using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Update the current 
    /// </summary>
    /// <param name="audioClip"></param>
    public void Play(AudioClip audioClip)
    {
        audioSource.clip = audioClip;

        // Play audio via AudioSource
        audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
    }

    /// <summary>
    /// Changes the volume of the audioSource
    /// </summary>
    /// <param name="newValue">The new value of the volume</param>
    public void ChangeVolumeValue(float newValue)
    {
        audioSource.volume = newValue;
    }
}
