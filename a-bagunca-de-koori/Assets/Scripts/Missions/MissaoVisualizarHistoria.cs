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
    private string nomeMissao = "Conhecendo a Koori";
    private string descricaoMissao = "Veja a história da Koori pelo menos uma vez";

    void Start()
    {
        PlayerPrefs.SetString("MissaoKoori_Nome", nomeMissao);
        PlayerPrefs.SetString("MissaoKoori_Descricao", descricaoMissao);
        PlayerPrefs.Save();

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
        string estadoTexto = EstadoMissaoParaTexto(estadoMissao);
        PlayerPrefs.SetString("EstadoMissaoVisualizarHistoria_Texto", estadoTexto);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Conhecendo a Koori': " + estadoMissao);
    }

    private string EstadoMissaoParaTexto(EstadoMissao estado)
    {
        switch (estado)
        {
            case EstadoMissao.NaoIniciado:
                return "Não Iniciado";
            case EstadoMissao.EmAndamento:
                return "Em Andamento";
            case EstadoMissao.Concluido:
                return "Concluído";
            default:
                return "Desconhecido";
        }
    }
}
