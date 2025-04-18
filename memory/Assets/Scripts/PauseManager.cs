using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inGameUI;

    private List<GameObject> cartoesPrefabsObjects = new List<GameObject>();

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

        ColetarTodosCartoes();
    }

    private void ColetarTodosCartoes()
    {
        // Limpa os cartões já existentes e procura novamente por objetos CardController para o array
        cartoesPrefabsObjects.Clear();
        CardController[] listaCartoes = FindObjectsOfType<CardController>();

        foreach (CardController card in listaCartoes)
        {
            // Percorre todos os cartões encontrados e adiciona ao array
            cartoesPrefabsObjects.Add(card.gameObject);
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
        
        // Interrompe o tempo de execução
        Time.timeScale = 0f; 

        // Desativa todos os cartões armazenados
        foreach (GameObject card in cartoesPrefabsObjects)
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

        // Reativa todos os cartões
        foreach (GameObject card in cartoesPrefabsObjects)
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

    public void CarregarTelaInicial()
    {
        Debug.Log("Função de ir para a Home será implementada futuramente.");
    }
}
