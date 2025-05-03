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
