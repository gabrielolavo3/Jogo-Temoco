using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmarAcao : MonoBehaviour
{
    [Header("GameObject do Pop-Up")]
    public GameObject infoPauseImage;
    public Text avisoText;
    public Button confirmarBtn;
    public Button cancelarBtn;

    private string acaoSolicitada = "";
    private string nomeCena;
    private PauseManager pauseManager;

    void Start()
    {
        infoPauseImage.SetActive(false);
        nomeCena = SceneManager.GetActiveScene().name;
        pauseManager = FindObjectOfType<PauseManager>();

        cancelarBtn.onClick.AddListener(() => CancelarAcao());
    }

    public void SolicitacaoAceita(string tipoAcao)
    {
        acaoSolicitada = tipoAcao;
        infoPauseImage.SetActive(true);

        if (tipoAcao == "TelaInicial")
        {
            avisoText.text = "O jogo vai ser encerrado e voltar para o menu inicial. Deseja continuar?";
            confirmarBtn.onClick.RemoveAllListeners();
            confirmarBtn.onClick.AddListener(() => ConfirmarSaida());
        }
        else if (tipoAcao == "Reiniciar")
        {
            avisoText.text = "O jogo vai ser reiniciado e perder o progresso atual. Deseja continuar?";
            confirmarBtn.onClick.RemoveAllListeners();
            confirmarBtn.onClick.AddListener(() => ConfirmarReinicioJogo());
        }
    }

    private void ConfirmarSaida()
    {
        pauseManager.pausePanel.SetActive(false);

        if (pauseManager != null)
        {
            pauseManager.pausePanel.SetActive(false);
        }

        pauseManager.CarregarTelaInicial("TelaFases");
    }

    private void ConfirmarReinicioJogo()
    {
        pauseManager.pausePanel.SetActive(false);

        if (pauseManager != null)
        {
            pauseManager.pausePanel.SetActive(false);
        }

        pauseManager.ReiniciarJogo();
    }

    private void CancelarAcao()
    {
        infoPauseImage.SetActive(false);
    }
}
