using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip musicaDaCena1_2;
    public AudioClip musicaDaCena3;
    private AudioSource _audioSource;
    public static AudioPlayer instanciaAudioPlayer;

    private void Awake()
    {
        if (instanciaAudioPlayer == null)
        {
            instanciaAudioPlayer = this;
            DontDestroyOnLoad(gameObject);            
            _audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += CenaCarregada;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (_audioSource.clip == null)
        {
            _audioSource.clip = musicaDaCena1_2;
            _audioSource.Play();
        }       
    }

    private void CenaCarregada(Scene cena, LoadSceneMode modoCarregando)
    {
        if (cena.buildIndex == 0)
        {
            if (_audioSource.clip != musicaDaCena1_2)
            {
                _audioSource.Stop();
                _audioSource.clip = musicaDaCena1_2;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
    }    

    private void CenaInterrompida()
    {
        SceneManager.sceneLoaded -= CenaCarregada;
    }

    public void TocarSegundaMusica()
    {
        _audioSource.Stop();
        _audioSource.clip = musicaDaCena3;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
