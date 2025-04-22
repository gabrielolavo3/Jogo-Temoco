using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScore : MonoBehaviour
{
    private int _pontuacaoJogatina;
    private int _melhorPontuacao;
    [SerializeField] private Text _pontuacaoText;
    [SerializeField] private Text _pontuacaoFinalText;
    
    void Start()
    {
        Placares();
    }

    private void Placares()
    {
        // Resgatando os valores de pontos salvos anteriormente
        _pontuacaoJogatina = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);
        _melhorPontuacao = PlayerPrefs.GetInt("MelhorPontuacaoGeral", 0);

        if (_pontuacaoJogatina > _melhorPontuacao)
        {
            _melhorPontuacao = _pontuacaoJogatina;
            PlayerPrefs.SetInt("MelhorPontuacaoGeral", _melhorPontuacao); // Atualiza o valor da pontuação
            PlayerPrefs.Save(); // Salva a mudança de valor
        }

        AtualizarTextUI();
    }

    private void AtualizarTextUI()
    {
        _pontuacaoText.text = _pontuacaoJogatina.ToString() + "x";
        _pontuacaoFinalText.text = _melhorPontuacao.ToString() + "x";
    }
}
