using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip musicaDaCena1_2;
    public AudioClip musicaDaCena3;
    private AudioSource audioSource;
    public static AudioPlayer instanciaAudioPlayer;

    private void Awake()
    {
        if (instanciaAudioPlayer == null)
        {
            instanciaAudioPlayer = this;
            DontDestroyOnLoad(gameObject);            
            audioSource = GetComponent<AudioSource>();

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
            audioSource.Play();
        }       
    }

    private void CenaCarregada(Scene cena, LoadSceneMode modoCarregando)
    {
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

    private void CenaInterrompida()
    {
        SceneManager.sceneLoaded -= CenaCarregada;
    }

    public void TocarSegundaMusica()
    {
        audioSource.Stop();
        audioSource.clip = musicaDaCena3;
        audioSource.loop = true;
        audioSource.Play();
    }
}
