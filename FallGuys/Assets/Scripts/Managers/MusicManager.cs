using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    // Для БГ Лобби, БГ Арена, Поиск карты, Заполнение шкалы лиги
    // для остальных локально на том объекте, который издает этот звук
    [Header("Music")]
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip arenaMusic;
    [SerializeField] private AudioClip raceMusic;
    [SerializeField] private AudioClip honeycombMusic;
    [SerializeField] private AudioClip mapSearchMusic;
    [SerializeField] private AudioClip fillingLeagueScaleMusic;
    [SerializeField] private AudioClip winMusic;

    [Header("Sounds")]
    [SerializeField] private AudioClip buttonPressSound;
    [SerializeField] private AudioClip applyButtonPressSound;

    [Space]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundsAudioSource;

    [Space]
    [SerializeField] private AudioMixerGroup musicMixer;
    [SerializeField] private AudioMixerGroup soundMixer;

    private string musicKey = "MusicEnabled";
    [SerializeField]private bool musicEnabled;
    private string soundsKey = "SoundsEnabled";
    [SerializeField]private bool soundsEnabled;

    private bool loadingComplete;

    private float previousSoundsValue;

    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Load();
    }

    public bool GetMusicEnabledState()
    {
        return musicEnabled;
    }
    public bool GetSoundsEnabledState()
    {
        return soundsEnabled;
    }

    public void ToggleMusic(bool enable)
    {
        if (enable)
        {
            musicMixer.audioMixer.SetFloat("MusicVolume", 0f);
            musicEnabled = true;
        }
        else
        {
            musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
            musicEnabled = false;
        }

        Save();
    }

    public void ToggleSounds(bool enable)
    {
        if (enable)
        {
            soundMixer.audioMixer.SetFloat("SoundsVolume", 0f);
            soundsEnabled = true;
        }
        else
        {
            soundMixer.audioMixer.SetFloat("SoundsVolume", -80f);
            soundsEnabled = false;
        }

        Save();
    }

    public void PlayUIButtonSounds(Button _button)// вызывается из VibrationManager, т.к. там проверка по тапу на UI
    {
        if(_button.gameObject.name == "SoundsButton")
        {
            if (soundsEnabled)
            {
                ToggleSounds(false);
                return;
            }
            else ToggleSounds(true);
        }

        if (_button.gameObject.name == "ApplyButton")
        {
            soundsAudioSource.clip = applyButtonPressSound;
        }
        else if (soundsAudioSource.clip != buttonPressSound) soundsAudioSource.clip = buttonPressSound;

        soundsAudioSource.Play();
    }

    public void StopSoundsPlaying()// при открытии вин луз окна
    {
        soundMixer.audioMixer.GetFloat("SoundsVolume", out float value);
        previousSoundsValue = value;
        soundMixer.audioMixer.SetFloat("SoundsVolume", -80f);
    }
    public void ReturnPreviousSoundsValue()// вызывается кнопками перехода в лобби
    {
        soundMixer.audioMixer.SetFloat("SoundsVolume", previousSoundsValue);
    }

    private void PlayMusic(AudioClip audioClip, bool loop = true)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
        musicAudioSource.loop = loop;
    }

    public void StopMusicPlaying()
    {
        musicAudioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        PlayMusic(lobbyMusic);
    }
    public void PlayArenaMusic()
    {
        PlayMusic(arenaMusic);
    }

    public void PlayRaceMusic()
    {
        PlayMusic(raceMusic);
    }

    public void PlayHoneycombMusic()
    {
        PlayMusic(honeycombMusic);
    }

    public void PlayMapSearchMusic()
    {
        PlayMusic(mapSearchMusic);
    }
    public void PlayFillingLeagueScaleMusic()
    {
        PlayMusic(fillingLeagueScaleMusic);
    }

    public void PlayWinMusic()
    {
        PlayMusic(winMusic, false);
    }

    private void Load()
    {
        int musicValue = PlayerPrefs.GetInt(musicKey, 1);

        if (musicValue == 0) musicEnabled = false;
        else musicEnabled = true;

        int soundsValue = PlayerPrefs.GetInt(soundsKey, 1);

        if (soundsValue == 0) soundsEnabled = false;
        else soundsEnabled = true;

        ToggleMusic(musicEnabled);
        ToggleSounds(soundsEnabled);

        loadingComplete = true;
    }
    private void Save()
    {
        int musicValue;

        if (musicEnabled) musicValue = 1;
        else musicValue = 0;

        PlayerPrefs.SetInt(musicKey, musicValue);

        int soundsValue;

        if (soundsEnabled) soundsValue = 1;
        else soundsValue = 0;

        PlayerPrefs.SetInt(soundsKey, soundsValue);
    }
}
