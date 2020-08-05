using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause_menu_;
    public AudioMixer audio_mixer_;

    public void PauseGame()
    {
        pause_menu_.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pause_menu_.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetVolume(float value)
    {
        audio_mixer_.SetFloat("MainVolume", value);
    }
}
