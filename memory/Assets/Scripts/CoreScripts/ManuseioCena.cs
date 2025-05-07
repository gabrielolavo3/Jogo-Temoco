using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuseioCena : MonoBehaviour
{
    public CaixaDeDialogos caixaDeDialogos;
    public LevelController levelController;
    private int chanceSorteada;
    private bool exibindoHistoria = false;
    [SerializeField] [Range(0, 100)] private int chanceDeReverHistoria;

    void Start()
    {
        if (InfoJogo.JogoFoiReiniciado)
        {
            InfoJogo.JogoFoiReiniciado = false;
            IniciarFase();
            return;
        }

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

    private void Update()
    {
        Debug.Log("Chance sorteada: " + chanceSorteada);
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
        levelController.StartLevel(); // Gera os cartões
    }
}
