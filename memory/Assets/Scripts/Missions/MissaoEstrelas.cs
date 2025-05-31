using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoEstrelas : MonoBehaviour
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
        bool concluiu = false;
        bool emAndamento = false;

        foreach (var config in configuracoesDeFase)
        {
            int estrelas = PlayerPrefs.GetInt("EstrelasFase" + config.idFase, 0);

            if (estrelas >= 3)
            {
                concluiu = true;
                break;
            }
            else if (estrelas >= 1)
            {
                emAndamento = true;
            }
        }

        if (concluiu)
        {
            estadoMissao = EstadoMissao.Concluido;
        }
        else if (emAndamento)
        {
            estadoMissao = EstadoMissao.EmAndamento;
        }
        else
        {
            estadoMissao = EstadoMissao.NaoIniciado;
        }

        PlayerPrefs.SetInt("EstadoMissaoTresEstrelas", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Em busca das estrelas': " + estadoMissao);
    }
}
