using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inGameUI;
    //public Temporizador temporizador;
    [HideInInspector] public bool jogoEstaPausado = false;


    void Start()
    {
        if (pausePanel != null)
        {
            // Desativa o panel do menu de pause
            pausePanel.SetActive(false);
        }
            
        if (inGameUI != null)
        {
            // Mantém aativa a área de jogo
            inGameUI.SetActive(true);
        }            

        Time.timeScale = 1f;        
    }

    public void AtivarTelaDePause()
    {
        PausarJogo();
    }

    public void PausarJogo()
    {
        if (pausePanel != null)
        {
            jogoEstaPausado = true;
            pausePanel.SetActive(true);
            //temporizador.PausarTemporizador();
        }
        
        // Interrompe o tempo de execução
        Time.timeScale = 0f;
       
    }

    public void ContinuarJogo()
    {
        if (pausePanel != null)
        {
            jogoEstaPausado = false;
            pausePanel.SetActive(false);
            //temporizador.RetomarTemporizador();
        }
            
        Time.timeScale = 1f;

    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        string cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }


    public void CarregarTelaInicial(string nome_cena)
    {
        Time.timeScale = 1f;
        StartCoroutine(VoltarParaInicio(nome_cena));
    }

    private IEnumerator VoltarParaInicio(string nome_cena)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nome_cena);
    }
}
