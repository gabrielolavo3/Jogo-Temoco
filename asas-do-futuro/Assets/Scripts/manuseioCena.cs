using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuseioCena : MonoBehaviour
{
    public CaixaDeDialogos caixaDeDialogos;
    private int chanceSorteada;
    private bool exibindoHistoria = false;

    public GameObject personagem;
    public GameObject painelPergunta;
    public GameObject btnRespostaA;
    public GameObject btnRespostaB;
    public GameObject btnRespostaC;
    public GameObject btnRespostaD;
    public GameObject perguntaUI;

    [SerializeField] [Range(0, 100)] private int chanceDeReverHistoria;

    void Start()
    {

        bool primeiraVez = !PlayerPrefs.HasKey("JaVisualizouHistoria");

        if (primeiraVez)
        {
            ExibirHistoria();
            PlayerPrefs.SetInt("JaVisualizouHistoria", 1);
            PlayerPrefs.Save();
        }
        else
        {
            chanceSorteada = UnityEngine.Random.Range(0, 100);            

            if (chanceSorteada < chanceDeReverHistoria && chanceSorteada > 0)
            {
                ExibirHistoria();
            }
            else
            {
                IniciarFase();
            }
        }
    }

    private void ExibirHistoria()
    {
        if (exibindoHistoria)
        {
            return;
        }

        exibindoHistoria = true;
        Time.timeScale = 0f; // Pausa o tempo
        caixaDeDialogos.DialogoFinalizado += IniciarFase;
        caixaDeDialogos.ExibirDialogo();
    }

    private void IniciarFase()
{
    if (exibindoHistoria)
    {
        caixaDeDialogos.DialogoFinalizado -= IniciarFase;
        exibindoHistoria = false;
    }

    Time.timeScale = 1f; // Retoma o tempo

    // Ativa os elementos da fase
    personagem.SetActive(true);
    painelPergunta.SetActive(true);
    btnRespostaA.SetActive(true);
    btnRespostaB.SetActive(true);
    btnRespostaC.SetActive(true);
    btnRespostaD.SetActive(true);
    perguntaUI.SetActive(true);
}
}