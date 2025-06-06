using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoPequenasVitorias : MonoBehaviour
{
    public enum EstadoMissao
    {
        NaoIniciado,
        EmAndamento,
        Concluido
    }

    public EstadoMissao estadoMissao = EstadoMissao.NaoIniciado;
    private int metaPontos = 500;

    void Start()
    {
        AtualizarEstadoMissao();
    }

    public void AtualizarEstadoMissao()
    {
        int acumulado = PlayerPrefs.GetInt("PontuacaoAcumuladaGeral", 0);

        if (acumulado <= 0)
        {
            estadoMissao = EstadoMissao.NaoIniciado;
        }
        else if (acumulado < metaPontos)
        {
            estadoMissao = EstadoMissao.EmAndamento;
        }
        else
        {
            estadoMissao = EstadoMissao.Concluido;
        }

        PlayerPrefs.SetInt("EstadoMissaoPequenasVitorias", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Pequenas vitórias': " + estadoMissao + " (Total acumulado: " + acumulado + ")");
    }
}
