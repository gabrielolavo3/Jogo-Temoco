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
    private string cenaSaida = "temas";
    public string cenaReinicio;
    private pauseBox pauseBox;
    
    void Start()
    {
        infoPauseImage.SetActive(false);
        nomeCena = SceneManager.GetActiveScene().name;
        pauseBox = FindObjectOfType<pauseBox>();

        cancelarBtn.onClick.AddListener(() => CancelarAcao());
    }

    public void SolicitacaoAceita(string tipoAcao)
    {
        acaoSolicitada = tipoAcao;
        infoPauseImage.SetActive(true);

        if (tipoAcao == "TelaInicial")
        {
            avisoText.text = "Você está prestes a sair do jogo e voltar para o menu inicial. Deseja continuar?";
            confirmarBtn.onClick.RemoveAllListeners();
            confirmarBtn.onClick.AddListener(() => ConfirmarSaida(cenaSaida));
        }
        else if (tipoAcao == "Reiniciar")
        {
            avisoText.text = "Você está prestes a reiniciar o jogo. Deseja continuar?";
            confirmarBtn.onClick.RemoveAllListeners();
            confirmarBtn.onClick.AddListener(() => ConfirmarReinicioJogo(cenaReinicio));
        }
    }  

    private void ConfirmarSaida(string cena)
    {
        pauseBox.pausePanel.SetActive(false);
        infoPauseImage.SetActive(false);
        pauseBox.CarregarTelaInicial(cena);
    }

    private void ConfirmarReinicioJogo(string cena)
    {
        pauseBox.pausePanel.SetActive(false);
        infoPauseImage.SetActive(false);
        pauseBox.ReiniciarJogo(cena);
    }

    private void CancelarAcao()
    {
        infoPauseImage.SetActive(false);
    }
}