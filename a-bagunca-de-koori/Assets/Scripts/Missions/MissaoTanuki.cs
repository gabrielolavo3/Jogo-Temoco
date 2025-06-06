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
    private string nomeMissao = "Rápido como um tanuki";
    private string descricaoMissao = "Experimente jogar cada fase de A Bagunça de Koori";

    void Start()
    {
        PlayerPrefs.SetString("MissaoTanuki_Nome", nomeMissao);
        PlayerPrefs.SetString("MissaoTanuki_Descricao", descricaoMissao);
        PlayerPrefs.Save();

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
        string estadoTexto = EstadoMissaoParaTexto(estadoMissao);
        PlayerPrefs.SetString("EstadoMissaoTanuki_Texto", estadoTexto);

        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Tanuki': " + estadoMissao);
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
