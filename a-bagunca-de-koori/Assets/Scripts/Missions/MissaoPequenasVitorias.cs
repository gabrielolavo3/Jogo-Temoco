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
    private string nomeMissao = "Pequenas vitórias";
    private string descricaoMissao = "Alcance 500 pontos jogando";

    void Start()
    {
        PlayerPrefs.SetString("MissaoVitorias_Nome", nomeMissao);
        PlayerPrefs.SetString("MissaoVitorias_Descricao", descricaoMissao);
        PlayerPrefs.Save();

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
        string estadoTexto = EstadoMissaoParaTexto(estadoMissao);
        PlayerPrefs.SetString("EstadoMissaoPequenasVitorias_Texto", estadoTexto);
        PlayerPrefs.Save();

        Debug.Log("Estado da Missão 'Pequenas vitórias': " + estadoMissao + " (Total acumulado: " + acumulado + ")");
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
