using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoJogoPerfeito : MonoBehaviour
{
    public enum EstadoMissao
    {
        NaoIniciado,
        EmAndamento,
        Concluido
    }

    public EstadoMissao estadoMissao = EstadoMissao.NaoIniciado;

    public PontuacaoConfigFase[] configuracoesDeFase;

    void Start()
    {
        AtualizarEstadoMissao();
    }

    public void AtualizarEstadoMissao()
    {
        int faseAtual = PlayerPrefs.GetInt("FaseAtual", 1);
        int pontuacaoFinal = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);

        PontuacaoConfigFase config = null;

        foreach (var c in configuracoesDeFase)
        {
            if (c.idFase == faseAtual)
            {
                config = c;
                break;
            }
        }

        if (config == null)
        {
            Debug.LogWarning("Configuração da fase não encontrada para a missão de jogo perfeito!");
            return;
        }

        int metaPerfeita = config.pontosMaximos + 50;

        if (pontuacaoFinal >= metaPerfeita)
        {
            estadoMissao = EstadoMissao.Concluido;
        }
        else if (pontuacaoFinal > 0)
        {
            estadoMissao = EstadoMissao.EmAndamento;
        }
        else
        {
            estadoMissao = EstadoMissao.NaoIniciado;
        }

        PlayerPrefs.SetInt("EstadoMissaoJogoPerfeito", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Jogo Perfeito': " + estadoMissao + " | Pontuação final: " + pontuacaoFinal + " | Meta: " + metaPerfeita);
    }
}
