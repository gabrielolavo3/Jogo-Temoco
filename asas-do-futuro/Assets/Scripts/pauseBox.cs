using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseBox : MonoBehaviour
{
    public GameObject pausePanel;
    //private bool MissaoReiniciar;
   
    //public Temporizador temporizador;
    [HideInInspector] public bool jogoEstaPausado = false;

    private List<GameObject> cartoesPrefabsObjects = new List<GameObject>();

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

    public void ReiniciarJogo(string nome_cena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nome_cena);
        //MissaoReiniciar = PlayerPrefs.GetInt(1);
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