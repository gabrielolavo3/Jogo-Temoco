using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inGameUI;

    private List<GameObject> _cartoesPrefabsObjects = new List<GameObject>();

    void Start()
    {
        if (pausePanel != null)
        {
            // Desativa o panel do menu de pause
            pausePanel.SetActive(false);
        }
            
        if (inGameUI != null)
        {
            // Mant�m aativa a �rea de jogo
            inGameUI.SetActive(true);
        }            

        Time.timeScale = 1f;

        ColetarTodosCartoes();
    }

    private void ColetarTodosCartoes()
    {
        // Limpa os cart�es j� existentes e procura novamente por objetos CardController para o array
        _cartoesPrefabsObjects.Clear();
        CardController[] listaCartoes = FindObjectsOfType<CardController>();

        foreach (CardController card in listaCartoes)
        {
            // Percorre todos os cart�es encontrados e adiciona ao array
            _cartoesPrefabsObjects.Add(card.gameObject);
        }
    }

    public void AtivarTelaDePause()
    {
        PausarJogo();
    }

    public void PausarJogo()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        
        // Interrompe o tempo de execu��o
        Time.timeScale = 0f; 

        // Desativa todos os cart�es armazenados
        foreach (GameObject card in _cartoesPrefabsObjects)
        {
            if (card != null)
            {
                card.SetActive(false);
            }                
        }
    }

    public void ContinuarJogo()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
            
        Time.timeScale = 1f;

        // Reativa todos os cart�es
        foreach (GameObject card in _cartoesPrefabsObjects)
        {
            if (card != null)
            {
                card.SetActive(true);
            }                
        }
    }

    public void ReiniciarJogo(string nome_cena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nome_cena);
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
