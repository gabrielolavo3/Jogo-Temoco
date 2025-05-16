using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressoJogo : MonoBehaviour
{
    [SerializeField] private PontuacaoConfigFase[] pontuacaoConfigFases;

    public int CalcularProgressoConclusao()
    {
        int fasesConcluidas = 0;

        foreach (var item in pontuacaoConfigFases)
        {
            int id = item.idFase;
            int pontuacaoSalva = PlayerPrefs.GetInt("MelhorPontuacaoFase" + id, 0);
            int acessos = PlayerPrefs.GetInt("AcessosFase" + id, 0);

            if (acessos >= 1 && pontuacaoSalva >= item.pontosMinimos && acessos >= item.acessoMinimo)
            {
                fasesConcluidas++;
            }
        }

        int porcentagemProgresso = (fasesConcluidas * 100) / pontuacaoConfigFases.Length;
        PlayerPrefs.SetInt("Conclusao", porcentagemProgresso);
        PlayerPrefs.Save();

        return porcentagemProgresso;
    }
}
