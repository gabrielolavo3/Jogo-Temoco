using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoTanuki : MonoBehaviour
{
    public enum EstadoMissao
    {
        NaoIniciado,
        EmAndamento,
        Concluido
    }

    public EstadoMissao estadoMissao = EstadoMissao.NaoIniciado;

    void Start()
    {
        AtualizarEstadoMissao();
    }

    public void AtualizarEstadoMissao()
    {
        bool fase1 = PlayerPrefs.GetInt("AcessosFase1", 0) > 0;
        bool fase2 = PlayerPrefs.GetInt("AcessosFase2", 0) > 0;
        bool fase3 = PlayerPrefs.GetInt("AcessosFase3", 0) > 0;

        int fasesJogadas = (fase1 ? 1 : 0) + (fase2 ? 1 : 0) + (fase3 ? 1 : 0);

        if (fasesJogadas == 0)
        {
            estadoMissao = EstadoMissao.NaoIniciado;
        }
        else if (fasesJogadas < 3)
        {
            estadoMissao = EstadoMissao.EmAndamento;
        }
        else
        {
            estadoMissao = EstadoMissao.Concluido;
        }

        PlayerPrefs.SetInt("EstadoMissaoTanuki", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Tanuki': " + estadoMissao);
    }
}
