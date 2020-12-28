using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    public AudioClip ambientClip;
    public AudioClip musicClip;

    public AudioClip[] walkStepsClip;
    public AudioClip[] crouchStepsClip;
    public AudioClip jumpClip;
    public AudioClip jumpVoiceClip;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;


    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);
        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    void StartLevelAudio()
    {
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }


    public static void PlyerFootStepAudio()
    {
        int index = Random.Range(0, current.walkStepsClip.Length);
        current.playerSource.clip = current.walkStepsClip[index];
        current.playerSource.Play();
    }

    public static void PlyerCrouchFootStepAudio()
    {
        int index = Random.Range(0, current.crouchStepsClip.Length);
        current.playerSource.clip = current.crouchStepsClip[index];
        current.playerSource.Play();
    }

    public static void playJumpAudio()
    {
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();
        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }
}
