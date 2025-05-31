using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMissoes : MonoBehaviour
{    
    void Start()
    {
        ImprimirEstadoMissaoTanuki();
        ImprimirEstadoMissaoPequenasVitorias();
        ImprimirEstadoMissaoVisualizarHistoria();
        ImprimirEstadoMissaoTresEstrelas();
    }

    private void ImprimirEstadoMissaoTanuki()
    {
        int estado = PlayerPrefs.GetInt("EstadoMissaoTanuki", 0);
        string estadoTexto = "";

        switch (estado)
        {
            case 0:
                estadoTexto = "Não iniciado";
                break;
            case 1:
                estadoTexto = "Em andamento";
                break;
            case 2:
                estadoTexto = "Concluído";
                break;
            default:
                estadoTexto = "Desconhecido";
                break;
        }

        Debug.Log("Estado da Missão 'Rápido como um Tanuki': " + estadoTexto);
    }

    private void ImprimirEstadoMissaoPequenasVitorias()
    {
        int estado = PlayerPrefs.GetInt("EstadoMissaoPequenasVitorias", 0);
        string estadoTexto = "";

        switch (estado)
        {
            case 0:
                estadoTexto = "Não iniciado";
                break;
            case 1:
                estadoTexto = "Em andamento";
                break;
            case 2:
                estadoTexto = "Concluído";
                break;
            default:
                estadoTexto = "Desconhecido";
                break;
        }

        Debug.Log("Estado da Missão 'Pequenas vitórias': " + estadoTexto);
    }

    private void ImprimirEstadoMissaoVisualizarHistoria()
    {
        int estado = PlayerPrefs.GetInt("EstadoMissaoVisualizarHistoria", 0);
        string estadoTexto = "";

        switch (estado)
        {
            case 0:
                estadoTexto = "Não iniciado";
                break;
            case 1:
                estadoTexto = "Em andamento"; // Como só visualiza ou não, podemos ignorar esse estado.
                break;
            case 2:
                estadoTexto = "Concluído";
                break;
            default:
                estadoTexto = "Desconhecido";
                break;
        }

        Debug.Log("Estado da Missão 'Prática leva à perfeição': " + estadoTexto);
    }

    private void ImprimirEstadoMissaoTresEstrelas()
    {
        int estado = PlayerPrefs.GetInt("EstadoMissaoTresEstrelas", 0);
        string estadoTexto = "";

        switch (estado)
        {
            case 0:
                estadoTexto = "Não iniciado";
                break;
            case 1:
                estadoTexto = "Em andamento"; // Aqui podemos ignorar, pois a missão é sim/não.
                break;
            case 2:
                estadoTexto = "Concluído";
                break;
            default:
                estadoTexto = "Desconhecido";
                break;
        }

        Debug.Log("Estado da Missão 'Em busca das estrelas': " + estadoTexto);
    }

}
