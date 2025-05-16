using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip musicaDaCena1_2;
    public AudioClip musicaDaCena3;
    public AudioSource audioSource;
    public static AudioPlayer instanciaAudioPlayer;

    private void Awake()
    {
        if (instanciaAudioPlayer == null)
        {
            instanciaAudioPlayer = this;
            DontDestroyOnLoad(gameObject);            
            //audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += CenaCarregada;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource.clip == null)
        {
            audioSource.clip = musicaDaCena1_2;
            audioSource.loop = true;
            audioSource.Play();
        }       
    }

    private void CenaCarregada(Scene cena, LoadSceneMode modoCarregando)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource não encontrado no AudioPlayer!");
                return;
            }
        }

        if (cena.buildIndex == 0)
        {
            if (audioSource.clip != musicaDaCena1_2)
            {
                audioSource.Stop();
                audioSource.clip = musicaDaCena1_2;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    //private void CenaInterrompida()
    //{
    //    SceneManager.sceneLoaded -= CenaCarregada;
    //}

    public void AlterarMusica()
    {
        TrocarMusica(musicaDaCena3);
    }

    private void TrocarMusica(AudioClip novaMusica)
    {
        audioSource.Stop();
        audioSource.clip = novaMusica;
        audioSource.loop = true;
        audioSource.Play();
    }
}