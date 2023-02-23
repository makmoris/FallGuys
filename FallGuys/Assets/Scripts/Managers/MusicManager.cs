using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    // ��� �� �����, �� �����, ����� �����, ���������� ����� ����
    // ��� ��������� �������� �� ��� �������, ������� ������ ���� ����
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip arenaMusic;
    [SerializeField] private AudioClip arenaMapSearchMusic;
    [SerializeField] private AudioClip fillingLeagueScaleMusic;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusic(true);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleMusic(false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleSounds(false);
            VibrationManager.Instance.ToogleVibration(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleSounds(true);
            VibrationManager.Instance.ToogleVibration(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            musicMixer.audioMixer.GetFloat("MusicVolume", out float val);
            Debug.Log($"MUSIC ENABLED FALSE; {val}");
        }
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
            Debug.Log("MUSIC ENABLED TRUE");
        }
        else
        {
            musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
            musicEnabled = false;
            musicMixer.audioMixer.GetFloat("MusicVolume", out float val);
            Debug.Log($"MUSIC ENABLED FALSE; {val}");
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

    public void PlayUIButtonSounds(Button _button)// ���������� �� VibrationManager, �.�. ��� �������� �� ���� �� UI
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

        soundsAudioSource.Play();
    }

    private void PlayMusic(AudioClip audioClip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
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
    public void PlayArenaMapSearchMusic()
    {
        PlayMusic(arenaMapSearchMusic);
    }
    public void PlayFillingLeagueScaleMusic()
    {
        PlayMusic(fillingLeagueScaleMusic);
    }

    private void Load()
    {
        int musicValue = PlayerPrefs.GetInt(musicKey, 1);

        if (musicValue == 0) musicEnabled = false;
        else musicEnabled = true;

        int soundsValue = PlayerPrefs.GetInt(soundsKey, 1);

        if (soundsValue == 0) soundsEnabled = false;
        else soundsEnabled = true;

        Debug.Log($"Music enabled = {musicEnabled}");

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
