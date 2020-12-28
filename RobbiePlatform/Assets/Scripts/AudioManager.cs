using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    public AudioClip winClip;
    public AudioClip startLevelClip;

    public AudioClip doorFXClip;

    public AudioClip orbFxClip;
    public AudioClip orbVoiceClip;


    public AudioClip deathFXClip;
    public AudioClip deathClip;
    public AudioClip deathVoiceClip;

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
        if(current != null)
        {
            Destroy(gameObject);
            return;
        }
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

        current.fxSource.clip = current.startLevelClip;
        current.fxSource.Play();
    }

    public static void PlayerWonAudio()
    {
        current.fxSource.clip = current.winClip;
        current.fxSource.Play();
        current.playerSource.Stop();
    }

    public static void PlayDoorOpenAudio()
    {
        current.fxSource.clip = current.doorFXClip;
        current.fxSource.PlayDelayed(1.1f);
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

    public static void playDeathAudio()
    {
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();
        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();
        current.fxSource.clip = current.deathFXClip;
        current.fxSource.Play();
    }

    public static void playOrbAudio()
    {
        current.fxSource.clip = current.orbFxClip;
        current.fxSource.Play();
        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();
    }
}
