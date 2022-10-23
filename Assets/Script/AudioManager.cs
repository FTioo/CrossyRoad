using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider bgmSlider;
    public AudioMixer mixer;

    public void Start() {
        float db;
        if (masterSlider == null)
            return;
        if (sfxSlider == null)
            return;
        if (bgmSlider == null)
            return;

        if (mixer.GetFloat("Master_Volume", out db))
            sfxSlider.value = (db+80)/80;    

        if (mixer.GetFloat("SFX_Volume", out db))
            sfxSlider.value = (db+80)/80;    

        if (mixer.GetFloat("BGM_Volume", out db))
            sfxSlider.value = (db+80)/80;    
    }

    public void MasterVolume(float value){
        value = value*80 - 80;

        mixer.SetFloat("Master_Volume",value);
    }
    public void SFXVolume(float value){
        value = value*80 - 80;

        mixer.SetFloat("SFX_Volume",value);
    }
    public void BGMVolume(float value){
        value = value*80 - 80;

        mixer.SetFloat("BGM_Volume",value);
    }

    public void muteToggle(bool Muted){
        if (Muted){
            AudioListener.volume = 1;
        }
        else{
            AudioListener.volume = 0;
        }

    }
}
