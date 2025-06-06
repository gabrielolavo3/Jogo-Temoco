using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MissaoInfo
{
    public string titulo;
    public string descricao;
    public string estadoKey; // Chave para buscar no PlayerPrefs
}

public class ControladorMenuMissoes : MonoBehaviour
{
    [Header("Informações das Missões")]
    public MissaoInfo[] missoes;

    [Header("Referências de UI")]
    public Text tituloText;
    public Text descricaoText;
    public Text estadoText;

    [Header("Partes Visuais")]
    public GameObject painelEstado;
    public GameObject painelTitulo;

    [Header("Mensagem Padrão")]
    public Text textoPadrao;

    [Header("Botões das Missões")]
    public Button[] botoesMissoes;

    [Header("Botão de Fechar")]
    public Button fecharBtn;
    public GameObject menuDeTarefas;

    private const string mensagemPadrao = "Selecione uma missão para ver detalhes.";

    void Start()
    {
        // Configura os listeners dos botões
        for (int i = 0; i < botoesMissoes.Length; i++)
        {
            int index = i; // evitar closure
            botoesMissoes[i].onClick.AddListener(() => ExibirMissao(index));
        }

        fecharBtn.onClick.AddListener(FecharMenu);

        // Estado inicial
        painelTitulo.SetActive(false);
        painelEstado.SetActive(false);

        textoPadrao.text = mensagemPadrao;
        textoPadrao.gameObject.SetActive(true);
        descricaoText.gameObject.SetActive(false);
    }

    void ExibirMissao(int index)
    {
        MissaoInfo missao = missoes[index];

        // Atualiza os textos
        tituloText.text = missao.titulo;
        descricaoText.text = missao.descricao;

        // Mostra descrição e oculta mensagem padrão
        descricaoText.gameObject.SetActive(true);
        textoPadrao.gameObject.SetActive(false);

        // Salva o nome da missão
        PlayerPrefs.SetString("UltimaMissaoSelecionada", missao.titulo);

        // Obtém e exibe o estado da missão
        int estado = PlayerPrefs.GetInt(missao.estadoKey, 0);
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

        estadoText.text = estadoTexto;

        // Atualiza visibilidade
        painelTitulo.SetActive(true);
        painelEstado.SetActive(true);

        // Salva o estado atual da missão também
        PlayerPrefs.SetInt("UltimoEstadoMissaoSelecionada", estado);
        PlayerPrefs.Save();
    }

    public void FecharMenu()
    {
        menuDeTarefas.SetActive(false);
    }

    public void AbrirMenu()
    {
        menuDeTarefas.SetActive(true);

        // Resetar para o estado padrão ao abrir
        painelTitulo.SetActive(false);
        painelEstado.SetActive(false);

        textoPadrao.text = mensagemPadrao;
        textoPadrao.gameObject.SetActive(true);
        descricaoText.gameObject.SetActive(false);
    }
}
