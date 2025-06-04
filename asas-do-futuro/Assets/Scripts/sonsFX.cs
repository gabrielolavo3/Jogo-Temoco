using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonsFX : MonoBehaviour
{
    public static sonsFX instanciaFX;
    [Range(0f, 1f)] public float volume = 1f;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        if (instanciaFX == null)
        {
            instanciaFX = this;
            //DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarEfeito(AudioClip efeito)
    {
        if (efeito != null)
        {
            audioSource.PlayOneShot(efeito, volume);
        }
    }
}
