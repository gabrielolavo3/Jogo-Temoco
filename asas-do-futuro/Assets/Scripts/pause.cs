using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool jogoEstaPausado = false;


    void Start()
    {
        if (pausePanel != null)
        {
            // Desativa o panel do menu de pause
            pausePanel.SetActive(false);
        }
                
        Time.timeScale = 1f;

    }


    public void AtivarTelaDePause()
    {
        ControlePause();
    }



    private bool jogoFoiPausado = false;

    public void ControlePause()
    {
        jogoFoiPausado = !jogoFoiPausado;

        if (jogoFoiPausado)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    internal void ReiniciarJogo(string v)
    {
        throw new NotImplementedException();
    }
}
