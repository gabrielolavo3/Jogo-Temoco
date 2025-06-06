using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoGuerreiroDoCeu : MonoBehaviour
{
    private int idTema;
    public enum EstadoMissao
    {
        NaoIniciado,
        EmAndamento,
        Concluido
    }

    public EstadoMissao estadoMissao = EstadoMissao.NaoIniciado;
    private int metaPontos = 1000;

    void Start()
    {
        AtualizarEstadoMissao();
    }

    public void AtualizarEstadoMissao()
    {
        int acumulado = PlayerPrefs.GetInt("notaFinalTemp" + idTema.ToString());

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

        PlayerPrefs.SetInt("EstadoMissaoGuerreiroDoCeu", (int)estadoMissao);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Guerreiro Do Céu': " + estadoMissao + " (Total acumulado: " + acumulado + ")");
    }
}

