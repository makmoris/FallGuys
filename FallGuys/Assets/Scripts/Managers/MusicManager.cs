using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Для БГ Лобби, БГ Арена, Поиск карты, Заполнение шкалы лиги
    // для остальных локально на том объекте, который издает этот звук
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip arenaMusic;
    [SerializeField] private AudioClip arenaMapSearchMusic;
    [SerializeField] private AudioClip fillingLeagueScaleMusic;

    private AudioSource _audioSource;

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

        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayerMusic(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public void PlayLobbyMusic()
    {
        PlayerMusic(lobbyMusic);
    }
    public void PlayArenaMusic()
    {
        PlayerMusic(arenaMusic);
    }
    public void PlayArenaMapSearchMusic()
    {
        PlayerMusic(arenaMapSearchMusic);
    }
    public void PlayFillingLeagueScaleMusic()
    {
        PlayerMusic(fillingLeagueScaleMusic);
    }
}
