using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AudioType
{
    music,
    gameSound
}

public class AudioManager : MonoBehaviour
{
    public List<Audio> audioObjects;
    public AudioClip backgroundAudio;
    public AudioClip platypusAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        audioObjects = new List<Audio>();

        // Create and add audio objects
        audioObjects.Add(new Audio("Background_Music", AudioType.music, backgroundAudio));
        audioObjects.Add(new Audio("Platypus_Noise", AudioType.gameSound, platypusAudio));

        // Set volume values
        ChangeVolumeValue(AudioType.music, gameObject.GetComponent<UIManager>().pausePanel.transform.Find("musicVolumeSlider").GetComponent<Slider>().value);
        ChangeVolumeValue(AudioType.gameSound, gameObject.GetComponent<UIManager>().pausePanel.transform.Find("gameSoundVolumeSlider").GetComponent<Slider>().value);

        Play("Background_Music");
    }

    void Update()
	{
        
    }

    /// <summary>
    /// Find the right child object to play the clip
    /// </summary>
    /// <param name="audioName"></param>
    public void Play(string audioName)
	{
        Audio audio = FindAudio(audioName);
		GameObject audioPlayer = null;

        switch(audio.AudioType) {
            case AudioType.music:
                audioPlayer = gameObject.transform.Find("audioObjBackgroundMusic").gameObject;
                break;
            case AudioType.gameSound:
                audioPlayer = gameObject.transform.Find("audioObjGameSounds").gameObject;
                break;
        }

        audioPlayer.GetComponent<AudioPlayer>().Play(audio.AudioClip);
    }

    /// <summary>
    /// Finds the first audioClip of the given name
    /// </summary>
    /// <param name="audioClipName">The name of the wanted Audio object</param>
    /// <returns>Returns the named Audio object, or null if none
    /// have that given name</returns>
    public Audio FindAudio(string audioClipName)
	{
        // Loops through every Audio object in the list and 
        // compares its name to the given string
        foreach(Audio audio in audioObjects) {
            if(audio.Name == audioClipName)
                return audio;
        }

        return null;
    }

    /// <summary>
    /// Changes the volume of the certain audioType
    /// </summary>
    /// <param name="audioType">The audioType volume being changed</param>
    /// <param name="newValue">The new value of the volume</param>
    public void ChangeVolumeValue(AudioType audioType, float newValue)
    {
        GameObject audioPlayer = null;
        switch(audioType) {
            case AudioType.music:
                audioPlayer = gameObject.transform.Find("audioObjBackgroundMusic").gameObject;
                break;
            case AudioType.gameSound:
                audioPlayer = gameObject.transform.Find("audioObjGameSounds").gameObject;
                break;
        }

        audioPlayer.GetComponent<AudioPlayer>().audioSource.volume = newValue;
    }
}
