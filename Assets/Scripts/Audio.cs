using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio
{
    private string name;
    private AudioType audioType;
    private AudioClip audioClip;

    public string Name { get { return name; } }
    public AudioType AudioType { get { return audioType; } }
    public AudioClip AudioClip { get { return audioClip; } }

    public Audio(string name, AudioType audioType, AudioClip audioClip)
	{
        this.name = name;
        this.audioType = audioType;
        this.audioClip = audioClip;
    }
}
