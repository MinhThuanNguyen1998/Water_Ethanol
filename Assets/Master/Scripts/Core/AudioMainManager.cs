using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
public enum SoundType
{
    Button,
    Popup,
    GasIgnition,
    BoilingWater,
    Step0,
    Step1,
    Step2,
    Step3
}
public class AudioMainManager : SingletonNotBaseSource<AudioMainManager>
{
    [Header("Audio Effects")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioPopupClip;
    [SerializeField] private AudioClip m_AudioButtonClip;
    [SerializeField] private AudioClip m_AudioGasIgnitionClip;
    [SerializeField] private AudioClip m_AudioBoilingWater;
    [SerializeField] private AudioClip m_AudioDoc;

    [Header("Audio Tutorials")]
    private Dictionary<SoundType, AudioClip> m_SoundMap;
    private bool isDocPaused = false;
    private void Awake()
    {
        m_SoundMap = new Dictionary<SoundType, AudioClip>
        {
            { SoundType.Button, m_AudioButtonClip },
            { SoundType.Popup, m_AudioPopupClip },
            { SoundType.GasIgnition, m_AudioGasIgnitionClip },
            { SoundType.BoilingWater, m_AudioBoilingWater },
        };
    }
    public void PlayOnShot(SoundType soundType)
    {
        if(m_SoundMap.TryGetValue(soundType, out var clip) && clip != null) m_AudioSource?.PlayOneShot(clip);
        else Debug.LogWarning($"AudioManager: AudioClip for {soundType} is not assigned.");
    }
    public void PlayAudioIntroduction(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
    public void PlayLoop(SoundType soundType)
    {
        if (m_SoundMap.TryGetValue(soundType, out var clip) && clip != null && m_AudioSource != null)
        {
            m_AudioSource.loop = true;
            m_AudioSource.clip = clip;
            m_AudioSource.Play();
        }
    }
    public void StopLoop()
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.Stop();
            m_AudioSource.loop = false;
            m_AudioSource.clip = null;
        }
    }
 
}
