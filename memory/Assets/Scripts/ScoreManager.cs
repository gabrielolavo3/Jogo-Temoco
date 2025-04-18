using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _pontuacaoTotalJogo;
    private int _quantAcertos;
    private int _quantErros;
    private int _pontoAdicional;

    [SerializeField] private Text _placarTotalText;
    [SerializeField] private Text _placarAcertoText;
    [SerializeField] private Text _placarErrosText;

    void Start()
    {
        _pontuacaoTotalJogo = 0;
        _quantAcertos = 0;
        _quantErros = 0;
        _pontoAdicional = 100;
    }

    public void AddAcerto()
    {
        // Incrementa a quantidade de acertos e adiciona os pontos ganhos
        _quantAcertos++;
        _pontuacaoTotalJogo += _pontoAdicional;
        AtualizarTextUI();
    }

    public void AddErro()
    {
        // Incrementa a quantidade de acertos e remove metade do total de pontos ganhos por acerto
        _quantErros++;
        _pontuacaoTotalJogo -= (_pontoAdicional / 2);

        if (_pontuacaoTotalJogo <= 0)
        {
            _pontuacaoTotalJogo = 0;
        }

        AtualizarTextUI();
    }

    private void AtualizarTextUI()
    {
        _placarTotalText.text = _pontuacaoTotalJogo.ToString() + "x";
        _placarAcertoText.text = _quantAcertos.ToString() + "x";
        _placarErrosText.text = _quantErros.ToString() + "x";
    }

    public void SalvarPontuacao()
    {
        // Subescreve e salvar a pontuação do jogo atual 
        PlayerPrefs.SetInt("UltimaPontuacaoTotal", _pontuacaoTotalJogo);
        PlayerPrefs.Save();
    }

    public void ReconfigurarPlacares()
    {
        _pontuacaoTotalJogo = 0;
        _quantAcertos = 0;
        _quantErros = 0;
        AtualizarTextUI();
    }
}
