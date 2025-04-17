using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Painel de pausa
    public GameObject inGameUI;   // HUD ou área do jogo

    void Start()
    {
        // Garante que o painel de pause esteja desativado ao iniciar
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }            

        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
        }
            
        Time.timeScale = 1f; // Garante que o tempo esteja rodando normalmente
    }

    public void ActivePause()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
            
        if (inGameUI != null)
        {
            inGameUI.SetActive(false);
        }            

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }            

        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
        }
            
        Time.timeScale = 1f;
    }

    public void RestartGame(string scene_name)
    {
        Time.timeScale = 1f; // Garante que o tempo volte ao normal
        SceneManager.LoadScene(scene_name);
    }    

    public void GoHome()
    {
        Debug.Log("Função de ir para a Home será implementada futuramente.");
        // Aqui você pode adicionar uma cena futura ou menu inicial
    }
}
