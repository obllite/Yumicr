using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance_;
    public AudioSource audio_source_, bgm_audio_source_;
    [SerializeField]
    private AudioClip jump_audio_, hurt_audio_, collect_audio_;

    private void Awake()
    {
        instance_ = this;
    }
    public void JumpAudio()
    {
        audio_source_.clip = jump_audio_;
        audio_source_.Play();
    }

    public void HurtAudio()
    {
        audio_source_.clip = hurt_audio_;
        audio_source_.Play();
    }

    public void CollectAudio()
    {
        audio_source_.clip = collect_audio_;
        audio_source_.Play();
    }

    public void PauseBGM()
    {
        bgm_audio_source_.enabled = false;
    }
}
