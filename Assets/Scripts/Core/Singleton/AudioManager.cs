using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum SFXType
{
    ButtonClick,
    StickObject
}
[Serializable] 
public struct SoundSFXData
{
    public SFXType Key;
    public AudioClip Sound;
}
public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;   // Nhạc nền
    [SerializeField] private AudioSource sfxSource;     // Hiệu ứng (SFX)
    [SerializeField] private AudioSource narrationSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> narrationClips;
    [SerializeField] private List<SoundSFXData> soundsFXes;
    private readonly Dictionary<SFXType, AudioClip> SoundSFXMap = new();

    private void Start()
    {
        InitDictionary();
    }

    private void InitDictionary()
    {
        foreach(var item in soundsFXes)
        {
            SoundSFXMap.Add(item.Key, item.Sound);
        }
    }
    public void PlaySFXButtonClick()
    {
        PlaySFX(SoundSFXMap[SFXType.ButtonClick]);
    }

    public void PlaySFXInMap(SFXType type)
    {
        PlaySFX(SoundSFXMap[type]);
    }

    // Phát nhạc nền
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopMusic( float duration)
    {
        musicSource.DOFade(0f, duration).OnComplete(() =>
        {
            musicSource.Stop();
            musicSource.volume = 1f; // reset volume để phát lại bình thường
        });
    }
    // Phát am thanh thuuyet minh
    public void PlayNarration(AudioClip clip)
    {
        if (clip == null) return;
        narrationSource.Stop();
        narrationSource.PlayOneShot(clip);
    }
    // Phat am thanh SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
    // Bật/tắt nhạc nền
    public void ToggleMusic(bool isOn)
    {
        musicSource.mute = !isOn;
    }
    // Bật/tắt SFX
    public void ToggleSFX(bool isOn)
    {
        sfxSource.mute = !isOn;
    }
    // Điều chỉnh âm lượng
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        narrationSource.volume = volume;
    }
    public void PlayNarration(int i)
    {
        if(i >= narrationClips.Count)
        {
            Debug.Log("Out index audio clip");
            return;
        }
        PlayNarration(narrationClips[i]);
    }
    public void PlayNarrationClip(AudioClip clip)
    {
        PlayNarration(clip);
    }
    public  void StopAll()
    {
        musicSource.Stop();
        sfxSource.Stop();
        narrationSource.Stop();
    }
}
