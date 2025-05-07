using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int pontuacaoTotalJogo;
    private int quantAcertos;
    private int quantErros;
    private int pontoAdicional;

    [SerializeField] private Text placarTotalText;
    [SerializeField] private Text placarAcertoText;
    [SerializeField] private Text placarErrosText;

    void Start()
    {
        pontuacaoTotalJogo = 0;
        quantAcertos = 0;
        quantErros = 0;
        pontoAdicional = 100;
    }

    public void AdicionarAcerto()
    {
        // Incrementa a quantidade de acertos e adiciona os pontos ganhos
        quantAcertos++;
        pontuacaoTotalJogo += pontoAdicional;
        AtualizarTextUI();
    }

    public void AdicionarErro()
    {
        // Incrementa a quantidade de acertos e remove metade do total de pontos ganhos por acerto
        quantErros++;
        pontuacaoTotalJogo -= (pontoAdicional / 2);

        if (pontuacaoTotalJogo <= 0)
        {
            pontuacaoTotalJogo = 0;
        }

        AtualizarTextUI();
    }

    private void AtualizarTextUI()
    {
        placarTotalText.text = pontuacaoTotalJogo.ToString() + "x";
        placarAcertoText.text = quantAcertos.ToString() + "x";
        placarErrosText.text = quantErros.ToString() + "x";
    }

    public void SalvarPontuacao()
    {
        // Subescreve e salvar a pontuação do jogo atual 
        PlayerPrefs.SetInt("UltimaPontuacaoTotal", pontuacaoTotalJogo);

        int faseAtual = PlayerPrefs.GetInt("FaseAtual", 1);
        string pontuacaoMaximaFase = "MelhorPontuacaoFase" + faseAtual;
        int pontuacaoAnteriorFase = PlayerPrefs.GetInt(pontuacaoMaximaFase, 0);

        if (pontuacaoTotalJogo > pontuacaoAnteriorFase)
        {
            PlayerPrefs.SetInt(pontuacaoMaximaFase, pontuacaoTotalJogo);
        }

        if (faseAtual == 1 && pontuacaoTotalJogo >= 300)
        {
            PlayerPrefs.SetInt("FaseDesbloqueada", Mathf.Max(2, PlayerPrefs.GetInt("FaseDesbloqueada", 1)));
        }
        else if (faseAtual == 2 && pontuacaoTotalJogo >= 500)
        {
            PlayerPrefs.SetInt("FaseDesbloqueada", Mathf.Max(3, PlayerPrefs.GetInt("FaseDesbloqueada", 2)));
        }

        PlayerPrefs.Save();
    }

    public void ReconfigurarPlacares()
    {
        pontuacaoTotalJogo = 0;
        quantAcertos = 0;
        quantErros = 0;
        AtualizarTextUI();
    }
}
