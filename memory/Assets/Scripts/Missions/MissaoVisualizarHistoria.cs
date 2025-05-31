using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoVisualizarHistoria : MonoBehaviour
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
        int jaVisualizou = PlayerPrefs.GetInt("JaVisualizouHistoria", 0);

        if (jaVisualizou == 1)
        {
            estadoMissao = EstadoMissao.Concluido;
        }
        else
        {
            estadoMissao = EstadoMissao.NaoIniciado;
        }

        PlayerPrefs.SetInt("EstadoMissaoVisualizarHistoria", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Conhecendo a Koori': " + estadoMissao);
    }
}
