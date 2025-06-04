using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpFinal : MonoBehaviour
{
    [Header("UI do Pop-up")]
    public GameObject painelPopup;
    public Text textoAviso;
    public Button botaoCancelar;
    public Button botaoConfirmar;
    
    private string acaoSolicitada = "";
    private string nomeCenaAtual;

    void Start()
    {
        painelPopup.SetActive(false);
        nomeCenaAtual = SceneManager.GetActiveScene().name;

        botaoCancelar.onClick.AddListener(FecharPopup);
    }

    public void SolicitarConfirmacao(string tipoAcao)
    {
        acaoSolicitada = tipoAcao;
        painelPopup.SetActive(true);

        botaoConfirmar.onClick.RemoveAllListeners();

        if (tipoAcao == "Reiniciar")
        {
            string ultimaFase = PlayerPrefs.GetString("UltimaFase");

            textoAviso.text = "O jogo vai ser reiniciado e retornar para a fase anterior. Deseja continuar?";
            botaoConfirmar.onClick.AddListener(() => ReiniciarCenaJogo(ultimaFase));
        }
        else if (tipoAcao == "TelaInicial")
        {
            textoAviso.text = "O jogo vai ser encaminhado para o menu inicial. Deseja continuar?";
            botaoConfirmar.onClick.AddListener(() => CarregarCenaInicial("TelaFases"));
        }
    }

    private void ReiniciarCenaJogo(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    private void CarregarCenaInicial(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    private void FecharPopup()
    {
        painelPopup.SetActive(false);
    }
}
