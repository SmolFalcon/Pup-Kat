using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;


    public void AmbientVolume(float volume)
    {
        audioMixer.SetFloat("AmbientVol", volume);
    }
    public void MusicVolume (float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }
    public void FXVolume(float volume)
    {
        audioMixer.SetFloat("FXVol", volume);
    }
}
