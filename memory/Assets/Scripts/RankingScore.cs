using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScore : MonoBehaviour
{
    private int pontuacaoJogatina;
    private int melhorPontuacao;
    [SerializeField] private Text pontuacaoText;
    [SerializeField] private Text pontuacaoFinalText;
    
    void Start()
    {
        Placares();
    }

    private void Placares()
    {
        // Resgatando os valores de pontos salvos anteriormente
        pontuacaoJogatina = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);
        melhorPontuacao = PlayerPrefs.GetInt("MelhorPontuacaoGeral", 0);

        if (pontuacaoJogatina > melhorPontuacao)
        {
            melhorPontuacao = pontuacaoJogatina;
            PlayerPrefs.SetInt("MelhorPontuacaoGeral", melhorPontuacao); // Atualiza o valor da pontuação
            PlayerPrefs.Save(); // Salva a mudança de valor
        }

        AtualizarTextUI();
    }

    private void AtualizarTextUI()
    {
        pontuacaoText.text = pontuacaoJogatina.ToString() + "x";
        pontuacaoFinalText.text = melhorPontuacao.ToString() + "x";
    }
}
